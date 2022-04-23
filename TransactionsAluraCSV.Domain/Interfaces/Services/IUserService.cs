using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Domain.Interfaces.Services
{
    public interface IUserService
    {
        void Register(User user);

        User LogIn(string email, string password);

    }
}
