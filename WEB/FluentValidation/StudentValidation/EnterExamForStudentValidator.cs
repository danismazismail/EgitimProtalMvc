using FluentValidation;
using WEB.Areas.Education.Models.ViewModels.Students;

namespace WEB.FluentValidation.StudentValidation
{
    public class EnterExamForStudentValidator : AbstractValidator<StudentDetailForExamsVM>
    {
        public EnterExamForStudentValidator()
        {
            RuleFor(x => x.Exam1)
                .GreaterThanOrEqualTo(0)
                .WithMessage("0 ya da daha büyük bir not girişi yapınız!")
                .LessThanOrEqualTo(100)
                .WithMessage("100 ya da daha küçük bir not girişi yapınız!");

            RuleFor(x => x.Exam2)
                .GreaterThanOrEqualTo(0)
                .WithMessage("0 ya da daha büyük bir not girişi yapınız!")
                .LessThanOrEqualTo(100)
                .WithMessage("100 ya da daha küçük bir not girişi yapınız!");

            RuleFor(x => x.ProjectExam)
                .GreaterThanOrEqualTo(0)
                .WithMessage("0 ya da daha büyük bir not girişi yapınız!")
                .LessThanOrEqualTo(100)
                .WithMessage("100 ya da daha küçük bir not girişi yapınız!");

        }
    }
}
