using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YourSpendings.Extensions;
using YourSpendings.Interfaces;
using YourSpendings.Models;
using YourSpendings.Models.ViewModels.Auth;
using YourSpendings.Models.ViewModels.ProfileViewModels;

namespace YourSpendings.Services
{
    public class AuthService(
        ICurrentUserService currentUserService,
        IApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            var userId = _currentUserService.UserId.ParseToInt();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User not found");

            if (user.Password != model.CurrentPassword.Hash())
                return false;

            user.Password = model.NewPassword.Hash();

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeProfile(ChangeProfileViewModel model)
        {
            var userId = _currentUserService.UserId.ParseToInt();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User not found");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUser()
        {
            var userId = _currentUserService.UserId.ParseToInt();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }


        public async Task<User?> LoginAsync(string email, string password)
        {
            var passwordHash = password.Hash();
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.Trim().ToLower() == email.Trim().ToLower() && x.Password == passwordHash);

            if (findUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, findUser.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            }

            return findUser;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> RegisterAsync(RegisterViewModel register)
        {
            var isEmailExist = await _context.Users.AnyAsync(x => x.Email == register.Email);

            if (isEmailExist)
            {
                return false;
            }

            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email.Trim().ToLower(),
                Password = register.Password.Hash()
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
