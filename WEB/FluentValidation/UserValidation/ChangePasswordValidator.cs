using FluentValidation;
using WEB.Models.ViewModels.Users;

namespace WEB.FluentValidation.UserValidation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
              .NotEmpty()
              .WithMessage("Şifre alanı zorunludur!")
              .MinimumLength(3)
              .WithMessage("En az 3 karakter giriniz!");

            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .WithMessage("Şifre alanı zorunludur!")
               .MinimumLength(3)
               .WithMessage("En az 3 karakter giriniz!");

            RuleFor(x => x.CheckNewPassword)
                .NotEmpty()
                .WithMessage("Şifre alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter giriniz!")
                .Equal(x => x.NewPassword)
                .WithMessage("Şifreler eşleşmiyor!");
        }
    }
}
