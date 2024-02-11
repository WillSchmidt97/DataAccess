using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository
    {
        public IEnumerable<User> GetAll()
        {
            using(var connection = new SqlConnection("ConnectionString")) 
            {
                return connection.GetAll<User>();
            }
        }
    }
}