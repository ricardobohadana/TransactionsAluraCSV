using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Domain.Entities
{
    public class TransferGroup
    {
        public DateTime TransferDate { get; set; }
        public User User { get; set; }
        public int NumOfTransfers { get; set; }
        public DateTime RegisterDate { get; set; }

    }
}
