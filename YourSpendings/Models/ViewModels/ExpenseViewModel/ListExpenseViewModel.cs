namespace YourSpendings.Models.ViewModels.ExpenseViewModel
{
    public class ListExpenseViewModel
    {
        public List<ExpenseViewModel> Expenses { get; set; }

        public decimal TotalValue { get; set; }
    }
}
