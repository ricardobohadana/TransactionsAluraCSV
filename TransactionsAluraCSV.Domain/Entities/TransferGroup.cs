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
        public User user { get; set; }
        public List<Transfer> Transfers { get; set; }

    }
}
