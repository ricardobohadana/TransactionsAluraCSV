using System.ComponentModel.DataAnnotations;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, insira um email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, insira uma senha.")]
        public string Password { get; set; }
    }
}
