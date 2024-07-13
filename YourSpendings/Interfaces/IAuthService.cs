using YourSpendings.Models;
using YourSpendings.Models.ViewModels.Auth;
using YourSpendings.Models.ViewModels.ProfileViewModels;

namespace YourSpendings.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUser();
        Task<bool> ChangeProfile(ChangeProfileViewModel model);
        Task<bool> ChangePassword(ChangePasswordViewModel model);

        Task<bool> RegisterAsync(RegisterViewModel register);

        Task<User?> LoginAsync(string email, string password);

        Task LogoutAsync();
    }
}
