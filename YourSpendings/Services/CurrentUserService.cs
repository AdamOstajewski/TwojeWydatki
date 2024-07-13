using System.Security.Claims;
using YourSpendings.Interfaces;

namespace YourSpendings.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContext) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

        public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
