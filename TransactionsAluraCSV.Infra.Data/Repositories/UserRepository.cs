using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Infra.Data.Contexts;

namespace TransactionsAluraCSV.Infra.Data.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        private readonly PostgreSqlContext _postgreSqlContext;

        public UserRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
        {
            _postgreSqlContext = postgreSqlContext;
        }

        public User FindByCredentials(string email, string password)
        {
            return _postgreSqlContext.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            return _postgreSqlContext.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}
