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

            return View();
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
                };

                using (var reader = new StreamReader(File.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<TransferMap>();
                    transferList = csv.GetRecords<Transfer>().ToList();
                }
                _transferService.CreateTransfer(transferList, Guid.Parse(HttpContext.User.Identity.Name));
                
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
                return View();
        }

        // GET: TransactionController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }        
    }
}
