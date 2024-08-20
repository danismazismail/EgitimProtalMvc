using DTO.Concrete.Clasrooms;
using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Areas.Education.Models.ViewModels.Classrooms;

namespace WEB.FluentValidation.ClasroomValidation
{
    public class CreateClassroomValidator : AbstractValidator<CreateClassroomVM>
    {
        public CreateClassroomValidator()
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ığüşöçİĞÜŞÖÇ-]+$");


            RuleFor(x => x.ClassroomName)
                .NotEmpty()
                .WithMessage("Sınıf ad alanı boş geçilemez!")
                .MinimumLength(5)
                .WithMessage("En az 5 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, rakam ve \"-\" girebilirsiniz");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Açıklama alanı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(200)
                .WithMessage("En fazla 200 karakter girebilirsiniz!");

            RuleFor(x => x.TeacherId)
                .NotEmpty()
                .WithMessage("Öğretmen alanı boş geçilemez!");
        }
    }
}
