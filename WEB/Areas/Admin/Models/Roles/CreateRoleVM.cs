using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Admin.Models.Roles
{
    public class CreateRoleVM
    {
        [Display(Name = "Rol")]
        public string? Name { get; set; }
    }
}
