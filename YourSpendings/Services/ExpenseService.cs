using Microsoft.EntityFrameworkCore;
using YourSpendings.Extensions;
using YourSpendings.Interfaces;
using YourSpendings.Models;
using YourSpendings.Models.ViewModels.ExpenseViewModel;

namespace YourSpendings.Services
{
    public class ExpenseService(IApplicationDbContext context,
        ICurrentUserService currentUser) : IExpenseService
    {
        private readonly IApplicationDbContext _context = context;
        private readonly ICurrentUserService _currentUser = currentUser;

        public async Task AddExpenseAsync(CreateExpenseViewModel expenseViewModel)
        {
            var userId = _currentUser.UserId.ParseToInt();

            var newExpense = new Expense
            {
                UserId = userId,
                Value = expenseViewModel.Value,
                Description = expenseViewModel.Description
            };

            _context.Expenses.Add(newExpense);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ExpenseViewModel>> GetAllExpensesAsync()
        {

            var userId = _currentUser.UserId.ParseToInt();

            var expenses = await _context.Expenses.Where(x => x.UserId == userId).Select(e => new ExpenseViewModel
            {
                Id = e.Id,
                Value = e.Value,
                Description = e.Description
            }).ToListAsync();

            if(expenses == null)
                return [];

            return expenses;
        }

        public async Task<ExpenseViewModel?> GetExpenseByIdAsync(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id);

            if (expense == null || expense.UserId != _currentUser.UserId.ParseToInt())
                return null;

            var expenseViewModel = new ExpenseViewModel
            {
                Id = expense.Id,
                Value = expense.Value,
                Description = expense.Description
            };

            return expenseViewModel;
        }

        public async Task<decimal> GetTotalExpenseAsync()
        {
            var userId = _currentUser.UserId.ParseToInt();

            var total = await _context.Expenses.Where(x => x.UserId == userId).SumAsync(x => x.Value);

            return total;
        }

        public async Task RemoveExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id);

            if (expense == null || expense.UserId != _currentUser.UserId.ParseToInt())
                return;

            _context.Expenses.Remove(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(ExpenseViewModel expenseViewModel)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == expenseViewModel.Id);

            if (expense == null || expense.UserId != _currentUser.UserId.ParseToInt())
                return;

            expense.Value = expenseViewModel.Value;
            expense.Description = expenseViewModel.Description;

            await _context.SaveChangesAsync();
        }
    }
}
