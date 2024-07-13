using YourSpendings.Models.ViewModels.ExpenseViewModel;

namespace YourSpendings.Interfaces
{
    public interface IExpenseService
    {
        public Task<List<ExpenseViewModel>> GetAllExpensesAsync();
        public Task<decimal> GetTotalExpenseAsync();
        public Task AddExpenseAsync(CreateExpenseViewModel expenseViewModel);
        public Task RemoveExpenseAsync(int id);
        public Task UpdateExpenseAsync(ExpenseViewModel expenseViewModel);

        public Task<ExpenseViewModel?> GetExpenseByIdAsync(int id);
    }
}
