using Microsoft.AspNetCore.Mvc;
using YourSpendings.Interfaces;

namespace YourSpendings.Controllers
{
    public class BaseApiController() : Controller
    {
        private ICurrentUserService _currentUser;

        protected ICurrentUserService CurrentUser => _currentUser ??= HttpContext.RequestServices.GetService<ICurrentUserService>();

    }
}
