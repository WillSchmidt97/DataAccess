using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

// See https://aka.ms/new-console-template for more information
const string connectionString
= "Server=localhost,1433;Database=baltaIO;User ID=sa;Password=1q2w3e4r@#$";

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
    NEWID(),
    title,
    url,
    summary,
    order,
    description,
    featured)";
    

using (SqlConnection connection = new SqlConnection(connectionString)) {
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach (var item in categories) {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

Console.WriteLine("Hello, World!");
