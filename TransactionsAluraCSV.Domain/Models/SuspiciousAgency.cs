using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Models
{
    public class SuspiciousAgency
    {
        public string Agency { get; set; }

        public string Bank { get; set; }

        public decimal Movement { get; set; }
    }
}
