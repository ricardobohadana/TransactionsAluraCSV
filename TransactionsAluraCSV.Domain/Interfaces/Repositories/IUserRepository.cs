using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User FindByEmail(string email);
        User FindByCredentials(string email, string password);
    }
}
