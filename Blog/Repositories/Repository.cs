using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly SqlConnection _connection;
        public UserRepository(SqlConnection connection) => _connection = connection;
        public IEnumerable<T> GetAll() => _connection.GetAll<T>();
    }
}