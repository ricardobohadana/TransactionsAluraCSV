using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Infra.Data.Contexts;

namespace TransactionsAluraCSV.Infra.Data.Repositories
{
    public class TransferRepository: BaseRepository<Transfer>, ITransferRepository
    {

        private readonly PostgreSqlContext _postgreSqlContext;

        public TransferRepository(PostgreSqlContext postgreSqlContext): base(postgreSqlContext)
        {
            _postgreSqlContext = postgreSqlContext;
        }

        public List<Transfer> GetByDate(DateTime date)
        {
            return _postgreSqlContext.Transfers.Where(t => t.TransferDate.Date.Equals(date.Date)).ToList();
        }
    }
}
