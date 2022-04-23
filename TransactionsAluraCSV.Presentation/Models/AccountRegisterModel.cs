using System.ComponentModel.DataAnnotations;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage = "Por favor, insira um nome.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, insira um email.")]
        public string Email { get; set; }
    }
}
