using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using CsvHelper;
using CsvHelper.Configuration;
using TransactionsAluraCSV.Presentation.Models;
using System.Xml.Serialization;
using TransactionsAluraCSV.Domain.Models;

namespace TransactionsAluraCSV.Domain.Services
{
    public class FileService : IFileService
    {
        public List<Transfer> XmlReader(IFormFile File)
        {
            List<Transfer> transferList;
            Transacoes transacoes;
            var xmlSerializer = new XmlSerializer(typeof(Transacoes));
            using (var reader = new StreamReader(File.OpenReadStream()))
            {
                transacoes = (Transacoes)xmlSerializer.Deserialize(reader); 
            }

            transferList = transacoes.Transacao.ConvertAll(t =>
            {
                return new Transfer()
                {
                    DestinationBank = t.Destino.Banco,
                    DestinationAgency = t.Destino.Agencia,
                    DestinationAccount = t.Destino.Conta,
                    OriginBank = t.Origem.Banco,
                    OriginAgency = t.Origem.Agencia,
                    OriginAccount = t.Origem.Conta,
                    TransferAmount = Decimal.Parse(t.Valor),
                    TransferDate = DateTime.Parse(t.Data),
                };
            });
            return transferList;
        }

        public List<Transfer> CsvReader(IFormFile File)
        {
            List<Transfer> transferList;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null,
                ShouldSkipRecord = record => record.Record.Any(field => String.IsNullOrWhiteSpace(field) || String.IsNullOrEmpty(field))
            };

            using (var reader = new StreamReader(File.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<TransferMap>();
                transferList = csv.GetRecords<Transfer>().ToList();
            }

            return transferList;
        }
    }
}
