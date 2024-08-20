using ApplicationCore.Consts;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class GetStudentsForClassroomVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public string? ProjectPath { get; set; }
        public string? ProjectName { get; set; }
    }
}
