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
            return _postgreSqlContext.Transfers.Where(t =>
                t.TransferDate.DayOfYear.Equals(date.DayOfYear) &&
                t.TransferDate.Year.Equals(date.Year)).ToList();
        }

        public List<Transfer> GetByRegisterDate(DateTime registerDate)
        {
            return _postgreSqlContext.Transfers.Where(t =>
                t.RegisterDate.DayOfYear.Equals(registerDate.DayOfYear) &&
                t.RegisterDate.Year.Equals(registerDate.Year)).ToList();
        }

        public List<Transfer> GetByMonthAndYear(int month, int year)
        {
            return _postgreSqlContext.Transfers.Where(t => t.TransferDate.Year == year && t.TransferDate.Month == month).ToList();
        }

        public List<Transfer> GetByTransferDate(int day, int month, int year)
        {
            var data = _postgreSqlContext.Transfers.Where(t =>
                t.TransferDate.Year == year &&
                t.TransferDate.Month == month &&
                t.TransferDate.Day == day
            ).ToList();


            return data;
        }
    }
}
