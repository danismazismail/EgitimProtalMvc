using System.ComponentModel.DataAnnotations;

namespace WEB.Models.ViewModels.Users
{
    public class ForgotPasswordVM
    {
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}
