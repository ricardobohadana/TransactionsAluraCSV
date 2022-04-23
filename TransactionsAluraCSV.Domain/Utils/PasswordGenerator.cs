using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Utils
{
    public class PasswordGenerator
    {
        public static string GenerateSixDigitPassword()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }
    }
}
