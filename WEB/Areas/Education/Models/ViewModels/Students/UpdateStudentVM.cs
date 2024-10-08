﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class UpdateStudentVM
    {
        public Guid Id { get; set; }
        public Guid AppUserID { get; set; }

        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Sınıf")]
        public Guid? ClassroomId { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Resim")]
        public IFormFile? Image { get; set; }

        public List<SelectListItem>? Classrooms { get; set; }
    }
}
