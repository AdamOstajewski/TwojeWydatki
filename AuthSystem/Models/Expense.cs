using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kwota jest wymagana.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        public string Description { get; set; }

        public string UserId { get; set; } // Dodane pole UserId do powiązania z użytkownikiem

    }
}
