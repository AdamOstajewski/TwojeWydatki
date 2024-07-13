using System.ComponentModel.DataAnnotations;

namespace YourSpendings.Models.ViewModels.ExpenseViewModel
{
    public class CreateExpenseViewModel
    {
        [Display(Name = "Kwota")]
        public decimal Value { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
