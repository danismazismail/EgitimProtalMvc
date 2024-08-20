using FluentValidation;
using System.Text.RegularExpressions;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.FluentValidation.TeacherValidation
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherVM>
    {
        public UpdateTeacherValidator()
        {
            Regex regex = new Regex("^[a-zA-Z ığüşöçİĞÜŞÖÇ-]+$");


            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf ve \"-\" girebilirsiniz");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(200)
                .WithMessage("En fazla 200 karakter girebilirsiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf ve \"-\" girebilirsiniz");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-Mail alanı boş geçilemez!")
                .EmailAddress()
                .WithMessage("Lütfen mail formatında giriş yapınız!");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Doğum tarihi alanı boş geçilemez!");


            RuleFor(x => x.HireDate)
                .NotEmpty()
                .WithMessage("İşe giriş tarihi boş geçilemez!");

            RuleFor(x => x.Course)
                .NotEmpty()
                .WithMessage("Ders adı boş geçilemez!");
        }
    }
}
