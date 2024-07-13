using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using YourSpendings.Interfaces;
using YourSpendings.Models.ViewModels.Auth;

namespace YourSpendings.Controllers
{
    public class AuthController(IHttpContextAccessor httpContext, IAuthService authService) : BaseApiController()
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContext;
        private readonly IAuthService _authService = authService;

        [HttpGet]
        public IActionResult Login()
        {

            ViewBag.UserId = CurrentUser.UserId;

            return View();
        }

        public IActionResult Register()
        {

            ViewBag.UserId = CurrentUser.UserId;

            return View();
        }

        public async Task< IActionResult> Logout()
        {
            await _authService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.LoginAsync(model.Email, model.Password);

                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _authService.RegisterAsync(model);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
