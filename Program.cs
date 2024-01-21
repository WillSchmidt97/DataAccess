using System.Data;
using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using Models;

class Program {
    static void Main(string[] args) {
        const string connectionString
        = "Server=localhost,1433;Database=baltaIO;User ID=sa;Password=1q2w3e4r@#$";
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        builder.TrustServerCertificate = true;

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
            // ListCategories(connection);
            // CreateCategory(connection);
            // CreateManyCategories(connection);
            // UpdateCategory(connection);
            // ExecuteProcedure(connection);
            // ReadProcedure(connection);
            // ExecuteScalar(connection);
            // ReadView(connection);
            // OneToOne(connection);
            // OneToMany(connection);
            QueryMultiple(connection);
        }
    }

    static void ListCategories(SqlConnection connection) {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories) {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
    }

    static void CreateCategory(SqlConnection connection) {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon";
        category.Description = "Amazon AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var insert = @"INSERT INTO
        [Category]
        VALUES (
            @Id,
            @Title,
            @Url,
            @Summary,
            @Order,
            @Description,
            @Featured)";
        
            var rows = connection.Execute(insert, new{

                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
            });
            Console.WriteLine($"{rows} rows inserted");
    }

    static void CreateManyCategories(SqlConnection connection) {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon";
        category.Description = "Amazon AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var category2 = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "New Category";
        category.Url = "new-category";
        category.Description = "Category Description";
        category.Order = 9;
        category.Summary = "Category Summary";
        category.Featured = true;

        var insert = @"INSERT INTO
        [Category]
        VALUES (
            @Id,
            @Title,
            @Url,
            @Summary,
            @Order,
            @Description,
            @Featured)";
        
            var rows = connection.Execute(insert, new[]{
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
            });
            Console.WriteLine($"{rows} rows inserted");
    }

    static void UpdateCategory(SqlConnection connection) {
        var update = @"UPDATE [Category] SET [Title] = @Title WHERE [Id] = @Id";
        var rows = connection.Execute(update, new {
            Id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
            Title = "Frontend 2021"
        });
        Console.WriteLine($"{rows} rows updated");
    }   

    static void ExecuteProcedure(SqlConnection connection) {
        var procedure = "[spDeleteStudent]";
        var pars = new { StudentId = "a3ab860d-3206-4df0-be68-498be671b2d3" };
        var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

        Console.WriteLine($"{affectedRows} rows deleted");
    }

    static void ReadProcedure(SqlConnection connection) {
        var procedure = "[spGetCoursesByCategory]";
        var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
        var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

        foreach (var course in courses){
            Console.WriteLine($"{course.Id} - {course.Title}");
        }
    }  

    static void ExecuteScalar(SqlConnection connection) {
                var category = new Category();
        category.Title = "Amazon AWS";
        category.Url = "amazon";
        category.Description = "Amazon AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var insert = @"INSERT INTO
            [Category]
        OUTPUT inserted.[Id]
        VALUES (
            NEWID(),
            @Title,
            @Url,
            @Summary,
            @Order,
            @Description,
            @Featured)";
        
            var id = connection.ExecuteScalar<Guid>(insert, new{

                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
            });
            Console.WriteLine($"The inserted category was: {id}");
    }

    static void ReadView(SqlConnection connection) {
        var sql = "SELECT * FROM [vwCourses]";
        var courses = connection.Query(sql);
        foreach (var item in courses) {
                Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void OneToOne(SqlConnection connection) {
        var sql = @"
        SELECT
            *
        FROM 
            [CareerItem]
        INNER JOIN 
            [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

        var items = connection.Query<CareerItem, Course, CareerItem>(
            sql,
            (careerItem, course) => {
                careerItem.Course = course;
                return careerItem;
            }, splitOn: "Id");

        foreach (var item in items) {
            Console.WriteLine($"{item.Id} - {item.Title} - {item.Course.Title}");
        }
    }

    static void OneToMany(SqlConnection connection) {
        var sql = @"
            SELECT 
                [Career].[Id],
                [Career].[Title],
                [CareerItem].[CareerId],
                [CareerItem].[Title]
            FROM
                [Career]
            INNER JOIN
                [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
            ORDER BY    
                [Career].[Title]";

        var careers = new List<Career>();
        var items = connection.Query<Career, CareerItem, Career>(
            sql,
            (career, item) => {
                var c = careers.Where(c => c.Id == career.Id).FirstOrDefault();
                if (c == null) {
                    c = career;
                    c.CareerItems.Add(item);
                    careers.Add(c);
                }
                else {
                    c.CareerItems.Add(item);
                }
                return career;
            }, splitOn: "CareerId");

        foreach (var item in items) {
            Console.WriteLine($"{item.Title}");
            foreach (var careerItem in item.CareerItems) {
                Console.WriteLine($" - {careerItem.Title}");
            }
        }
    }

    static void QueryMultiple(SqlConnection connection) {
        var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

        using (var multi = connection.QueryMultiple(query)) {
            var categories = multi.Read<Category>();
            var courses = multi.Read<Course>();

            foreach(var category in categories) {
                Console.WriteLine($"{category.Title}");
            }

            foreach(var course in courses) {
                Console.WriteLine($"{course.Title}");
            }
        }
    }
}