using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Domain.Interfaces.Repositories
{
    public interface ITransferRepository: IBaseRepository<Transfer>
    {
        List<Transfer> GetByDate(DateTime date);
    }
}
