using System.ComponentModel.DataAnnotations;

namespace TransactionsAluraCSV.Presentation.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Por favor, insira um nome para este usuário.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, insira um email válido")]
        [Required(ErrorMessage = "Por favor, insira um nome para este usuário.")]
        public string Email { get; set; }
    }
}
