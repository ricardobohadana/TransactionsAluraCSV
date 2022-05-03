using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Domain.Models
{
    public class SuspiciousData
    {
        public SuspiciousData(List<Transfer> suspiciousTransfers, List<SuspiciousAccount> suspiciousAccounts, List<SuspiciousAgency> suspiciousAgencies)
        {
            SuspiciousTransfers = suspiciousTransfers;
            SuspiciousAccounts = suspiciousAccounts;
            SuspiciousAgencies = suspiciousAgencies;
        }

        public List<Transfer> SuspiciousTransfers { get; set; }
        
        public List<SuspiciousAccount> SuspiciousAccounts { get; set; }

        public List<SuspiciousAgency> SuspiciousAgencies { get; set; }

    }
}
