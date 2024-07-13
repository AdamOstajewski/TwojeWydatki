using YourSpendings.Models.Common;

namespace YourSpendings.Models.ViewModels.Auth
{
    public class LoginViewModel : ResponseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
