using System.ComponentModel.DataAnnotations;

namespace YourSpendings.Models.ViewModels.ProfileViewModels
{
    public class ChangeProfileViewModel
    {
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
