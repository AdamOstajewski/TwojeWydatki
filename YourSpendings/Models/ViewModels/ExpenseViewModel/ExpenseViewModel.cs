using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace YourSpendings.Models.ViewModels.ExpenseViewModel
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ilosc")]
        public decimal Value { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
