using System.ComponentModel.DataAnnotations;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class UserEditModel
    {

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        [Required(ErrorMessage = "Por favor, preencha este campo.")]
        public string Email { get; set; }
    }
}
