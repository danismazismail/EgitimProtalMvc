using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Models.ViewModels.Users;

namespace WEB.FluentValidation.UserValidation
{
    public class LoginValidator : AbstractValidator<LoginVM>
    {
        public LoginValidator()
        {
            Regex regex = new Regex("^[a-zA-Z-.]+$");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı alanı boş geçilemez!")
                .MaximumLength(256)
                .WithMessage("256 karakter sınırını geçtiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf ve \"-\" girebilirsiniz. Türkçe karakter kullanamazsınız!"); ;

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre alanı boş geçilemez!");
        }
    }
}
