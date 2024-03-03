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
            //ReadUsers(connection);
            //ReadRoles(connection);
            //ReadTags(connection);
            //CreateUser(connection);
            //CreateRole(connection);
            //CreateTag(connection);
            UpdateUser(connection);
            //UpdateRole(connection);
            //UpdateTag(connection);
            //DeleteUser(connection);
            connection.Close();
        }

        public static void ReadUsers(SqlConnection sqlConnection)
        {
            var repository = new Repository<User>(sqlConnection);
            var users = repository.GetAll();

            foreach(var user in users) 
                Console.WriteLine(user.Name);
        }

        public static void ReadUser(SqlConnection sqlConnection)
        {
            var repository = new Repository<User>(sqlConnection);
            var user = repository.Get(1);

            Console.WriteLine(user.Name);
        }

        public static void CreateUser(SqlConnection sqlConnection)
        {
            var repository = new Repository<User>(sqlConnection);

            repository.Create(new User
            {
                Name = "William Schmidt",
                Email = "billy.schmidt1997",
                PasswordHash = "123456",
                Bio = "I'm a software developer",
                Image = "https://randomuser.me/api/portraits",
                Slug = "bill-schmidt"
            });

            var newUser = repository.GetAll().Last();
            Console.WriteLine($"New user: {newUser.Name}");
        }      

        public static void UpdateUser(SqlConnection sqlConnection)
        {
            var repository = new Repository<User>(sqlConnection);
            var user = repository.Get(6);
            if (user != null) {
                user.Name = "Bill Schmidt";
                user.Email = "willschmidt1997@gmail.com";
                user.Bio = "I'm a .NET developer";
                repository.Update(user);

                Console.WriteLine($"User {user.Name} updated");
            }
            else
                Console.WriteLine("User not found");
        }

        public static void DeleteUser(SqlConnection sqlConnection)
        {
            var repository = new Repository<User>(sqlConnection);
            var user = repository.Get(6);
            if (user != null) {
                repository.Delete(user);

                Console.WriteLine("User deleted");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }

        public static void ReadRoles(SqlConnection sqlConnection)
        {
            var repository = new Repository<Role>(sqlConnection);
            var items = repository.GetAll();

            foreach(var item in items) 
                Console.WriteLine(item.Name);
        }

        public static void ReadRole(SqlConnection sqlConnection)
        {
            var repository = new Repository<Role>(sqlConnection);
            var item = repository.Get(1);

            Console.WriteLine(item.Name);
        }

        public static void CreateRole(SqlConnection sqlConnection)
        {
            var repository = new Repository<Role>(sqlConnection);

            repository.Create(new Role
            {
                Name = "Administrator",
                Slug = "main-administrator"
            });

            var newRole = repository.GetAll().Last();
            Console.WriteLine($"New role: {newRole.Name}");
        }

        public static void UpdateRole(SqlConnection sqlConnection)
        {
            var repository = new Repository<Role>(sqlConnection);
            var item = repository.Get(1);
            if (item != null) {
                item.Name = "Admin";
                item.Slug = "second-admin";
                repository.Update(item);

                Console.WriteLine($"Role {item.Name} updated");
            }
            else
                Console.WriteLine("Role not found");
        }

        public static void DeleteRole(SqlConnection sqlConnection)
        {
            var repository = new Repository<Role>(sqlConnection);
            var item = repository.Get(1);
            if (item != null) {
                repository.Delete(item);

                Console.WriteLine("Role deleted");
            }
            else
            {
                Console.WriteLine("Role not found");
            }
        }

        public static void ReadTags(SqlConnection sqlConnection)
        {
            var repository = new Repository<Tag>(sqlConnection);
            var tags = repository.GetAll();

            foreach(var tag in tags) 
                Console.WriteLine(tag.Name);
        }

        public static void ReadTag(SqlConnection sqlConnection)
        {
            var repository = new Repository<Tag>(sqlConnection);
            var tag = repository.Get(1);

            Console.WriteLine(tag.Name);
        }

        public static void CreateTag(SqlConnection sqlConnection)
        {
            var repository = new Repository<Tag>(sqlConnection);

            repository.Create(new Tag
            {
                Name = "C#",
                Slug = "c-sharp"
            });

            var newTag = repository.GetAll().Last();
            Console.WriteLine($"New tag: {newTag.Name}");
        }

        public static void UpdateTag(SqlConnection sqlConnection)
        {
            var repository = new Repository<Tag>(sqlConnection);
            var tag = repository.Get(1);

            if (tag != null) {
                tag.Name = "C# 9";
                tag.Slug = "c-sharp-9";
                repository.Update(tag);

                Console.WriteLine($"Tag {tag.Name} updated");
            }
            else
                Console.WriteLine("Tag not found");
        }

        public static void DeleteTag(SqlConnection sqlConnection)
        {
            var repository = new Repository<Tag>(sqlConnection);
            var tag = repository.Get(1);

            if (tag != null) {
                repository.Delete(tag);

                Console.WriteLine("Tag deleted");
            }
            else
            {
                Console.WriteLine("Tag not found");
            }
        }	        
    }
}	
