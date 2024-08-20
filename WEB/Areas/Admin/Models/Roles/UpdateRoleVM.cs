using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Admin.Models.Roles
{
    public class UpdateRoleVM
    {
        public Guid Id { get; set; }
        [Display(Name = "Rol")]
        public string? Name { get; set; }

    }
}
