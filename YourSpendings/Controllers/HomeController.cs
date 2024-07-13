using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YourSpendings.Models;

namespace YourSpendings.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : BaseApiController()
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            ViewBag.UserId = CurrentUser.UserId;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
