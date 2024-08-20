using FluentValidation;
using WEB.Models.ViewModels.Users;

namespace WEB.FluentValidation.UserValidation
{
    public class CreatePasswordValidator : AbstractValidator<CreatePasswordVM>
    {
        public CreatePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
               .NotEmpty()
               .WithMessage("Şifre alanı zorunludur!")
               .MinimumLength(4)
               .WithMessage("En az 4 karakter giriniz!")
               .Equal("1234")
               .WithMessage("Şifre Hatalı!");

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
