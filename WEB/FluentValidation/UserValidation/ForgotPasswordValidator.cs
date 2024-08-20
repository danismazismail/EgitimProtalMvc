using FluentValidation;
using WEB.Models.ViewModels.Users;

namespace WEB.FluentValidation.UserValidation
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordVM>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-Mail alanı zorunludur!")
                .EmailAddress()
                .WithMessage("E-Mail formatında giriş yapınız. Örnek: example@example.com");
        }
    }
}
