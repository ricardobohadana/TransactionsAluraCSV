using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Interfaces.Mail;

namespace TransactionsAluraCSV.Domain.Utils
{
    public class MailProvider: IMailProvider
    {
        public void SendPassword(string emailAddress, string password)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("dfc708e3d9b55c", "ff68f30586da7f"),
                EnableSsl = true
            };
            client.Send(
                "cacolorde@gmail.com",
                emailAddress,
                "Thank you!",
                $"We thank you for your interest in being a part of this sites' users. Here is your password: <strong> {password}</strong>"
            );
        }
    }
}
