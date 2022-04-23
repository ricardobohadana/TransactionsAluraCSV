using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Entities
{
    public class Transfer
    {
        public Guid TransferId { get; set; }

        public Guid UserId { get; set; }

        public string OriginBank { get; set; }

        public string OriginAgency { get; set; }

        public string OriginAccount { get; set; }

        public string DestinationBank { get; set; }

        public string DestinationAgency { get; set; }

        public string DestinationAccount { get; set; }

        public decimal TransferAmount { get; set; }

        public DateTime TransferDate { get; set; }

        public User User { get; set; }
    }
}
