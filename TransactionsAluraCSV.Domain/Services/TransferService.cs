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
        private readonly IUserRepository _userRepository;

        public TransferService(ITransferRepository transferRepository, IUserRepository userRepository)
        {
            _transferRepository = transferRepository;
            _userRepository = userRepository;
        }

        public void CreateTransfer(List<Transfer> transferList, Guid userId)
        {
            var groups = this.GetTransferGroups();

            var mainDate = transferList[0].TransferDate;

            // Evitar que arquivos do mesmo dia sejam enviados
            if (groups.Any(g => g.TransferDate.Date == mainDate)){
                throw new Exception($"Não é possível enviar um arquivo com transações para esta data ({mainDate.ToString("dd/MM/yyyy")}, pois já foram enviadas transações para este dia.)");
            }

            transferList = transferList.FindAll(transfer =>
            {
                // evitar que campos faltantes sejam cadastrados.
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

                // evitar que data distina à do início do arquivo seja incluída para cadastro.
                if (transfer.TransferDate.Date != mainDate.Date)
                {
                    return false;
                }
                return true;
            });

            // data de cadastro das transações
            var registerDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            transferList.ForEach(transfer =>
            {
                transfer.RegisterDate = registerDate;
                transfer.TransferDate = DateTime.SpecifyKind(transfer.TransferDate, DateTimeKind.Utc);
                transfer.TransferId = Guid.NewGuid();
                transfer.UserId = userId;
                _transferRepository.Insert(transfer);
            });
        }

        public List<TransferGroup> GetTransferGroups()
        {
            var transfers = _transferRepository.GetAll();
            var users = _userRepository.GetAll();

            transfers.ForEach(transfer => {
                transfer.User = users.Where(u => u.UserId.Equals(transfer.UserId)).FirstOrDefault();
            });

            return transfers.GroupBy(t => new { t.TransferDate.Date, t.User, t.RegisterDate }).Select(t => new TransferGroup()
            {
                TransferDate = t.Select(tt => tt.TransferDate).Min(),
                NumOfTransfers = t.Select(tt => tt.TransferId).ToList().Count(),
                User = t.Key.User,
                RegisterDate = t.Key.RegisterDate
            }).ToList();
        }
    }
}
