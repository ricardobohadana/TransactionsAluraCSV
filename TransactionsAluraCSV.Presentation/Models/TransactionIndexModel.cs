using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Models;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class TransactionIndexModel
    {
        public List<TransferGroup> TransferGroups { get; set; }
    }
}
