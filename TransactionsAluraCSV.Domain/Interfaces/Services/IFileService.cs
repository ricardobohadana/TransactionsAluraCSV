using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;

namespace TransactionsAluraCSV.Domain.Interfaces.Services
{
    public interface IFileService
    {
        List<Transfer> XmlReader(IFormFile File);
        List<Transfer> CsvReader(IFormFile File);
    }
}
