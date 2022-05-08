
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IFileService _fileService;

        public TransactionController(ITransferService transferService, IFileService fileService)
        {
            _transferService = transferService;
            _fileService = fileService;
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

            try
            {
                List<Transfer> transferList; 

                if (File.FileName.EndsWith(".csv"))
                {
                    transferList = _fileService.CsvReader(File);

                }else if (File.FileName.EndsWith(".xml"))
                {
                    transferList = _fileService.XmlReader(File);
                }
                else
                {
                    throw new Exception("Formato de arquivo não aceito! Por favor, envie um arquivo .csv ou .xml");
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

        public IActionResult Reports()
        {
            List<SelectListItem> months = new();
            for (int i = 1; i <= 12; i++)
            {
                var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                months.Add(new SelectListItem(monthName, $"{i}"));
            }

            TransactionReportsModel model = new()
            {
                MonthList = months,
                Year = DateTime.Now.Year
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Reports(TransactionReportsModel model)
        {
            try
            {
                int month = Int32.Parse(model.Month);
                if (month > 12 || month < 1) throw new Exception("O mês definido é inválido");
                var data = _transferService.GetSuspiciousMovements(month, model.Year);

                model.SuspiciousData = data;

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
            }

            List<SelectListItem> months = new();
            for (int i = 1; i <= 12; i++)
            {
                var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                months.Add(new SelectListItem(monthName, $"{i}"));
            }
            model.MonthList = months;
            return View(model);
        }
    }
}
