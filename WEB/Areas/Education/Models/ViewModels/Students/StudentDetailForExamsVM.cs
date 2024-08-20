using ApplicationCore.Consts;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class StudentDetailForExamsVM
    {
        public Guid Id { get; set; }
        
        public Guid AppUserID { get; set; }
        [Display(Name = "Ad")]
        public string FirstName { get; set; }


        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Sınıf Adı")]
        public string ClassroomName { get; set; }


        [Display(Name = "1. Sınav")]
        public double? Exam1 { get; set; }
        
        [Display(Name = "2. Sınav")]
        public double? Exam2 { get; set; }
        
        [Display(Name = "Proje Notu")]
        public double? ProjectExam { get; set; }


        [Display(Name = "Ortalama")]
        public double? Average { get; set; }

        [Display(Name = "Durum")]
        public StudentStatus StudentStatus { get; set; }


        [Display(Name = "Proje Adı")]
        public string? ProjectName { get; set; }
        public string? ImagePath { get; set; }
        public string? ProjectPath { get; set; }
        public Guid ClassroomId { get; set; }

    }
}
