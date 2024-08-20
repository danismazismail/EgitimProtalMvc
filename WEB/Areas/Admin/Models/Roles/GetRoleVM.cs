using ApplicationCore.Consts;

namespace WEB.Areas.Admin.Models.Roles
{
    public class GetRoleVM
    {
        public Guid? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
    }
}
