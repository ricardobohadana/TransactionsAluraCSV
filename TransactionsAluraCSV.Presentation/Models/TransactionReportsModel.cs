using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TransactionsAluraCSV.Domain.Models;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class TransactionReportsModel
    {
        public SuspiciousData SuspiciousData { get; set; }

        [Required(ErrorMessage = "Por favor, defina o mês de análise")]
        [Display(Name = "Mês")]
        public string Month { get; set; }

        [Display(Name = "Ano")]
        [Range(2000, 2023, ErrorMessage = "O {0} de análise deve estar entre {1} e {2}")]
        [Required(ErrorMessage = "Por favor, defina o ano de análise")]
        public int Year { get; set; }

        #region Campo de seleção do tipo DropDown

        public List<SelectListItem>? MonthList{ get; set; }

        #endregion
    }
}
