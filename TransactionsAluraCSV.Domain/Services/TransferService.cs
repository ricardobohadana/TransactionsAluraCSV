using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Domain.Models;

namespace TransactionsAluraCSV.Domain.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IUserRepository _userRepository;
        private readonly decimal _transferLimit = 100 * 1000;
        private readonly decimal _accountLimit = 1000 * 1000;
        private readonly decimal _agencyLimit = 1000 * 1000 * 1000;

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
            if (groups.Any(g => g.TransferDate.Date == mainDate.Date)){
                throw new Exception($"Não é possível enviar um arquivo com transações para esta data ({mainDate.ToString("dd/MM/yyyy")}), pois já foram enviadas transações para este dia.");
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

        public List<Transfer> GetTransfersByDate(DateTime date)
        {
            return _transferRepository.GetByRegisterDate(date);

        }

        public SuspiciousData GetSuspiciousMovements(int month, int year)
        {
            var transfers = _transferRepository.GetByMonthAndYear(month, year);
            
            // suspiciousTransfers
            var suspiciousTransfers = transfers.Where(t => t.TransferAmount >= 100*1000).ToList();

            // suspiciousAccounts
            var suspiciousAccounts = transfers.GroupBy(
                t => new { t.OriginBank, t.OriginAgency, t.OriginAccount }).Select(
                t => new SuspiciousAccount()
                {
                    Bank = t.Key.OriginBank,
                    Account = t.Key.OriginAccount,
                    Agency = t.Key.OriginAgency,
                    Movement = t.Select(tt => tt.TransferAmount).Sum()
                }
            ).ToList();

            var suspiciousDestinationAccounts = transfers.GroupBy(
                t => new { t.DestinationBank, t.DestinationAgency, t.DestinationAccount }).Select(
                t => new SuspiciousAccount()
                {
                    Bank = t.Key.DestinationBank,
                    Account = t.Key.DestinationAccount,
                    Agency = t.Key.DestinationAgency,
                    Movement = t.Select(tt => tt.TransferAmount).Sum()
                }
            ).ToList();

            //var suspiciousAccounts = new List<SuspiciousAccount>();
            suspiciousDestinationAccounts.ForEach(s1 => suspiciousAccounts.Add(s1));

            suspiciousAccounts = suspiciousAccounts.GroupBy(s => new { s.Bank, s.Agency, s.Account }).Select(s => new SuspiciousAccount()
            {
                Bank = s.Key.Bank,
                Account = s.Key.Account,
                Agency = s.Key.Agency,
                Movement = s.Select(ss => ss.Movement).Sum()
            }).ToList();


            suspiciousAccounts = suspiciousAccounts.Where(sa => sa.Movement >= 1000*1000).ToList();

            // suspiciousAgencies
            var suspiciousAgencies = transfers.GroupBy(
                t => new { t.OriginBank, t.OriginAgency }).Select(
                t => new SuspiciousAgency()
                {
                    Bank = t.Key.OriginBank,
                    Agency = t.Key.OriginAgency,
                    Movement = t.Select(tt => tt.TransferAmount).Sum()
                }
            ).ToList();

            var suspiciousDestinationAgencies = transfers.GroupBy(
                t => new { t.DestinationBank, t.DestinationAgency }).Select(
                t => new SuspiciousAgency()
                {
                    Bank = t.Key.DestinationBank,
                    Agency = t.Key.DestinationAgency,
                    Movement = t.Select(tt => tt.TransferAmount).Sum()
                }
            ).ToList();

            suspiciousDestinationAgencies.ForEach(s1 => suspiciousAgencies.Add(s1));

            suspiciousAgencies = suspiciousAgencies.GroupBy(s => new { s.Bank, s.Agency }).Select(s => new SuspiciousAgency()
            {
                Bank = s.Key.Bank,
                Agency = s.Key.Agency,
                Movement = s.Select(ss => ss.Movement).Sum()
            }).ToList();

            suspiciousAgencies = suspiciousAgencies.Where(sa => sa.Movement >= 1000*1000*1000).ToList();

            return new SuspiciousData(suspiciousTransfers, suspiciousAccounts, suspiciousAgencies);

        }
    }
}
