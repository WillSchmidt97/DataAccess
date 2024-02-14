using System;
using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string ConnectionString = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
        static void Main(string[] args)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
           ReadUsers(connection);
           ReadRoles(connection);
           // CreateUser();
           // UpdateUser();
           // DeleteUser();
           connection.Close();
        }

        public static void ReadUsers(SqlConnection sqlConnection)
        {
            var repository = new UserRepository(sqlConnection);
            var users = repository.GetAll();

            foreach(var user in users) 
                Console.WriteLine(user.Name);
        }

        public static void ReadRoles(SqlConnection sqlConnection)
        {
            var repository = new RoleRepository(sqlConnection);
            var roles = repository.GetAll();

            foreach(var role in roles) 
                Console.WriteLine(role.Name);
        }              
    }
}	
