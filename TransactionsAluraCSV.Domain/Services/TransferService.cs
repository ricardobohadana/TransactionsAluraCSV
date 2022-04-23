using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Domain.Interfaces.Services;

namespace TransactionsAluraCSV.Domain.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;

        public TransferService(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public void CreateTransfer(List<Transfer> transferList, Guid userId)
        {


            var mainDate = transferList[0].TransferDate;

            transferList = transferList.FindAll(transfer =>
            {

                if (
                    transfer.DestinationBank == "" || transfer.DestinationBank == null ||
                    transfer.DestinationAgency == "" || transfer.DestinationAgency == null ||
                    transfer.DestinationAccount == "" || transfer.DestinationAccount == null ||
                    transfer.OriginAccount == "" || transfer.OriginAccount == null ||
                    transfer.OriginAgency == "" || transfer.OriginAgency == null ||
                    transfer.OriginBank == "" || transfer.OriginBank == null ||
                    transfer.TransferAmount == 0 || transfer.TransferAmount == null ||
                    transfer.TransferDate == null
                )
                {
                    return false;
                }
                if (transfer.TransferDate.Date != mainDate.Date)
                {
                    return false;
                }
                return true;
            });

            transferList.ForEach(transfer =>
            {
                transfer.TransferId = Guid.NewGuid();
                transfer.UserId = userId;
                _transferRepository.Insert(transfer);
            });
        }
    }
}
