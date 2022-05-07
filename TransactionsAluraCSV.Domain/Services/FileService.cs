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

namespace TransactionsAluraCSV.Domain.Services
{
    public class FileService : IFileService
    {
        public List<Transfer> XmlReader(IFormFile File)
        {
            List<Transfer> transferList;
            var xmlSerializer = new XmlSerializer(typeof(List<Transfer>));
            using (var reader = new StreamReader(File.OpenReadStream()))
            {
                transferList = (List<Transfer>)xmlSerializer.Deserialize(reader); 
            }

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
