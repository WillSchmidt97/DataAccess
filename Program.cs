using System.Data;
using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

class Program {
    static void Main(string[] args) {
        const string connectionString
        = "Server=localhost,1433;Database=baltaIO;User ID=sa;Password=1q2w3e4r@#$";
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        builder.TrustServerCertificate = true;

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
            //ListCategories(connection);
            // CreateCategory(connection);
            //CreateManyCategories(connection);
            // UpdateCategory(connection);
            ExecuteProcedure(connection);
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
}