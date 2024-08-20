using ApplicationCore.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Teachers
{
    public class CreateTeacherVM
    {
        public CreateTeacherVM()
        {
            Courses = Enum.GetValues(typeof(Course)).Cast<Course>().Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = x.ToString()
            }).ToList();
        }

        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }



        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HireDate { get; set; }


        [Display(Name = "Ders")]
        public Course? Course { get; set; }

        public List<SelectListItem>? Courses { get; }
    }
}
