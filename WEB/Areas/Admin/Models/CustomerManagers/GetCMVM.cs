using ApplicationCore.Consts;

namespace WEB.Areas.Admin.Models.CustomerManagers
{
    public class GetCMVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
        
        public DateTime HireDate { get; set; }
    }
}
