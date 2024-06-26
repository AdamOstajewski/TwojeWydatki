using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

        public IActionResult Expenses()
        {
            var userId = _userManager.GetUserId(this.User);
            var allExpenses = _context.Expenses.Where(e => e.UserId == userId).ToList();

            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                model.UserId = _userManager.GetUserId(this.User);
                _context.Expenses.Add(model);
            }
            else
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == model.Id);
                if (expenseInDb != null)
                {
                    expenseInDb.Value = model.Value;
                    expenseInDb.Description = model.Description;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Expenses");
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            if (expenseInDb != null)
            {
                _context.Expenses.Remove(expenseInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("Expenses");
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
