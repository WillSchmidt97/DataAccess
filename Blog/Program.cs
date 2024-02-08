using System;
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string ConnectionString = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";
        static void Main(string[] args)
        {
            ReadUsers();
        }

        public static void ReadUsers()
        {
            using(var connection = new SqlConnection(ConnectionString)) {
                var users = connection.GetAll<User>();

                foreach(var user in users) {
                    Console.WriteLine(user.Name);
                }
            }
        }
    }
}	
