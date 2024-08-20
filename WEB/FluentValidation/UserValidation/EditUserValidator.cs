using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Models.ViewModels.Users;

namespace WEB.FluentValidation.UserValidation
{
    public class EditUserValidator : AbstractValidator<EditUserVM>
    {
        public EditUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-Mail alanı boş geçilemez!")
                .EmailAddress()
                .WithMessage("Lütfen mail formatında giriş yapınız!");
        }
    }
}
