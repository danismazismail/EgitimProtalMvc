namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class GetClassroomsForTeacherVM
    {
        public Guid Id { get; set; }
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public string ClassroomSize { get; set; }
    }
}
