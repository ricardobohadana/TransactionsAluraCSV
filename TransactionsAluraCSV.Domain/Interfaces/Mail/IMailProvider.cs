using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Interfaces.Mail
{
    public interface IMailProvider
    {
        Task SendPassword(string emailAddress, string password);
    }
}
