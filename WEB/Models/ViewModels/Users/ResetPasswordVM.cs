using System.ComponentModel.DataAnnotations;

namespace WEB.Models.ViewModels.Users
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        public string Password { get; set; }


        [Display(Name = "Şifre Tekrar")]
        public string CheckPassword { get; set; }
    }
}
