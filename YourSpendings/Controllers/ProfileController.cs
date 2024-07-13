using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourSpendings.Interfaces;
using YourSpendings.Models.ViewModels.ProfileViewModels;

namespace YourSpendings.Controllers
{
    [Authorize]
    public class ProfileController(
        ICurrentUserService currentUser,
        IAuthService authService) : BaseApiController
    {
        private ICurrentUserService _currentUser = currentUser;
        private IAuthService _authService = authService;

        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = _currentUser.UserId;

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ChangeProfile()
        {
            ViewBag.UserId = _currentUser.UserId;

            var user = await _authService.GetUser();

            var model = new ChangeProfileViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            ViewBag.UserId = _currentUser.UserId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(ChangeProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _authService.ChangeProfile(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _authService.ChangePassword(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
