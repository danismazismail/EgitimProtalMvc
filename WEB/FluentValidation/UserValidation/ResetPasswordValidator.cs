using FluentValidation;
using WEB.Models.ViewModels.Users;

namespace WEB.FluentValidation.UserValidation
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter giriniz!");

            RuleFor(x => x.CheckPassword)
                .NotEmpty()
                .WithMessage("Şifre alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter giriniz!")
                .Equal(x => x.Password)
                .WithMessage("Şifreler eşleşmiyor!");
        }
    }
}
