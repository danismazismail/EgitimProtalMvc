using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Areas.Admin.Models.Roles;

namespace WEB.FluentValidation.RoleValidation
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleVM>
    {
        public CreateRoleValidator()
        {

            Regex regex = new Regex("^[a-zA-Z-]+$");


            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Rol ad alanı boş geçilemez!")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter girmelisiniz!")
                .MaximumLength(255)
                .WithMessage("En fazla 255 karakter girebilirsiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf ve \"-\" girebilirsiniz. Türkçe karakter kullanamazsınız!");
        }
    }
}
