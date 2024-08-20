using ApplicationCore.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Teachers
{
    public class UpdateTeacherVM
    {
        public UpdateTeacherVM()
        {
            Courses = Enum.GetValues(typeof(Course)).Cast<Course>().Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = x.ToString(),
                Selected = x == Course //Enum değeri aşağıdaki Course propertysindeki değerle aynı gelsin.
            }).ToList();
        }
        public Guid Id { get; set; }

        public Guid AppUserID { get; set; }
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Ders")]
        public Course? Course { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }



        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HireDate { get; set; }

        public List<SelectListItem>? Courses { get; }
    }
}
