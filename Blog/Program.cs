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
            ReadTags(connection);
           // CreateUser();
           // UpdateUser();
           // DeleteUser();
            connection.Close();
        }

        public static void ReadUsers(SqlConnection sqlConnection)
        {
            var repository = new Repository<User>(sqlConnection);
            var users = repository.GetAll();

            foreach(var user in users) 
                Console.WriteLine(user.Name);
        }      

        public static void ReadRoles(SqlConnection sqlConnection)
        {
            var repository = new Repository<Role>(sqlConnection);
            var items = repository.GetAll();

            foreach(var item in items) 
                Console.WriteLine(item.Name);
        }

        public static void ReadTags(SqlConnection sqlConnection)
        {
            var repository = new Repository<Tag>(sqlConnection);
            var tags = repository.GetAll();

            foreach(var tag in tags) 
                Console.WriteLine(tag.Name);
        }        
    }
}	
