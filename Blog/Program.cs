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
           // ReadUsers();
           // CreateUser();
           // UpdateUser();
           // DeleteUser();
        }

        public static void ReadUsers()
        {
            var repository = new UserRepository();
            var users = repository.GetAll();

            foreach(var user in users) 
            {
                Console.WriteLine(user.Name);
            }
        }
        public static void ReadUser()
        {
            using(var connection = new SqlConnection(ConnectionString)) {
                var user = connection.Get<User>(1);
                Console.WriteLine(user.Name);
            }
        }
        public static void CreateUser()
        {
            var user = new User() {
                Bio = "I'm a software developer",
                Email = "willschmidt1997@gmail.com",
                Image = "https://www.google.com",
                Name = "Bill Schmidt",
                PasswordHash = "HASH",
                Slug = "bill-schmidt"
            };

            using(var connection = new SqlConnection(ConnectionString)) {
                connection.Insert<User>(user);
                Console.WriteLine($"User {user.Name} has been created");
            }
        }
        
        public static void UpdateUser()
        {
            var user = new User() {
                Id = 3,
                Bio = "I'm a backend developer",
                Email = "willschmidt1997@gmail.com",
                Image = "https://www.google.com",
                Name = "Will Schmidt",
                PasswordHash = "HASH",
                Slug = "will-schmidt"
            };

            using(var connection = new SqlConnection(ConnectionString)) {
                connection.Update<User>(user);
                Console.WriteLine($"User {user.Name} has been updated");
            }
        }   
        public static void DeleteUser()
        {
            using(var connection = new SqlConnection(ConnectionString)) {
                var user = connection.Get<User>(3);
                connection.Delete<User>(user);
                Console.WriteLine($"User {user.Name} has been deleted");
            }
        }               
    }
}	
