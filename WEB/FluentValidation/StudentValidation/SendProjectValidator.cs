using FluentValidation;
using WEB.Areas.Education.Models.ViewModels.Students;

namespace WEB.FluentValidation.StudentValidation
{
    public class SendProjectValidator : AbstractValidator<StudentDetailForProjectVM>
    {
        public SendProjectValidator()
        {
            RuleFor(x => x.ProjectName)
                .NotEmpty()
                .WithMessage("Proje adı zorunludur!");
        }
    }
}
