using System.ComponentModel.DataAnnotations;
using YourSpendings.Models.Common;

namespace YourSpendings.Models.ViewModels.Auth
{
    public class RegisterViewModel : ResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
