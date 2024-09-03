using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models.ViewModels
{
    public class UserProfileCreateViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}

