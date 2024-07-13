using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourSpendings.Interfaces;
using YourSpendings.Models.ViewModels.ExpenseViewModel;

namespace YourSpendings.Controllers
{
    [Authorize]
    public class ExpenseController(IExpenseService expanseService) : BaseApiController
    {
        private IExpenseService _expanseService = expanseService;

        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = CurrentUser.UserId;

            var expenses = await _expanseService.GetAllExpensesAsync();
            var total = await _expanseService.GetTotalExpenseAsync();

            var model = new ListExpenseViewModel
            {
                Expenses = expenses,
                TotalValue = total
            };

            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.UserId = CurrentUser.UserId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _expanseService.AddExpenseAsync(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _expanseService.RemoveExpenseAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var expanse = await _expanseService.GetExpenseByIdAsync(id);

            if (expanse == null)
                return RedirectToAction("Index");

            return View(expanse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _expanseService.UpdateExpenseAsync(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
