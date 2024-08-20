using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;

namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class GetClassroomVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public string TeacherName { get; set; }
        public int ClassroomSize { get; set; }
    }
}
