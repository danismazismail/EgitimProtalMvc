using ApplicationCore.Consts;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class GetStudentVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ClassroomName { get; set; }
        public string? TeacherName { get; set; }
        public double? Average { get; set; }
        public StudentStatus? StudentStatus { get; set; }
    }
}
