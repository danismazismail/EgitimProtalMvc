using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class UpdateClassroomVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Sınıf Adı")]
        public string? ClassroomName { get; set; }

        [Display(Name = "Açıklaması")]
        public string? Description { get; set; }

        [Display(Name = "Eğitmen")]
        public Guid? TeacherId { get; set; }

        public List<SelectListItem>? Teachers { get; set; }
    }
}
