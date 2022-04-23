using CsvHelper.Configuration;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class TransferMap : ClassMap<Transfer>
    {
        public TransferMap()
        {
            Map(m => m.OriginBank).Index(0);
            Map(m => m.OriginAgency).Index(1);
            Map(m => m.OriginAccount).Index(2);
            Map(m => m.DestinationBank).Index(3);
            Map(m => m.DestinationAgency).Index(4);
            Map(m => m.DestinationAccount).Index(5);
            Map(m => m.TransferAmount).Index(6);
            Map(m => m.TransferDate).Index(7);
        }
    }
}
