using DTO.Concrete.CustomerManagers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WEB.Areas.Admin.Models.CustomerManagers;

namespace WEB.FluentValidation.CustomerManagerValidation
{
    public class CreateCMValidator : AbstractValidator<CreateCMVM>
    {
        public CreateCMValidator()
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

         
        }
    }
}
