using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Presentation.Models;

namespace TransactionsAluraCSV.Presentation.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransferService _transferService;

        public TransactionController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        // GET: TransactionController
        public IActionResult Index()
        {
            var groups = _transferService.GetTransferGroups();

            TransactionIndexModel model = new()
            {
                TransferGroups = groups
            };

            return View(model);
        }

        // GET: TransactionController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        public IActionResult Create(IFormFile File)
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["MensagemErro"] = "Os dados inseridos estão incorretos, por favor verifique.";

            //    return View();
            //}

            try
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
                _transferService.CreateTransfer(transferList, Guid.Parse(HttpContext.User.Identity.Name));

                TempData["MensagemSucesso"] = $"Parabéns, as transações para o dia {transferList[0].TransferDate.ToString("dd/MM/yyyy")} foram cadastradas.";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
                return View();
        }

        public IActionResult Detail(string date)
        {
            try
            {
                DateTime registerDate = DateTime.SpecifyKind(DateTime.Parse(date), DateTimeKind.Utc);

                List<Transfer> transfersList = _transferService.GetTransfersByDate(registerDate);

                TransactionDetailModel model = new()
                {
                    Transfers = transfersList,
                };

                return View(model);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
                return RedirectToAction("Index");
            }
        }


        // GET: TransactionController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }        
    }
}
