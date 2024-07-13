using System.ComponentModel.DataAnnotations;

namespace YourSpendings.Models.ViewModels.ProfileViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Aktualne hasło")]
        public string CurrentPassword { get; set; }

        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
