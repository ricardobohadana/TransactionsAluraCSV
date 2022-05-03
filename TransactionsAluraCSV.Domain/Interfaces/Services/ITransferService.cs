using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Models;

namespace TransactionsAluraCSV.Domain.Interfaces.Services
{
    public interface ITransferService
    {
        void CreateTransfer(List<Transfer> transferList, Guid userId);

        List<TransferGroup> GetTransferGroups();

        List<Transfer> GetTransfersByDate(DateTime date);

        SuspiciousData GetSuspiciousMovements(int month, int year);
    }
}
