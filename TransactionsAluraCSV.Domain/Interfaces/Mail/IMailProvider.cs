using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Interfaces.Mail
{
    public interface IMailProvider
    {
        void SendPassword(string emailAddress, string password);
    }
}
