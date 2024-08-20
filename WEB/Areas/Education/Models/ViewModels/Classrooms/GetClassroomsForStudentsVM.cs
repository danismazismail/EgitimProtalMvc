namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class GetClassroomsForStudentsVM
    {
        public Guid Id { get; set; }
        public string ClassroomName { get; set; }
        public string TeacherName { get; set; }
        public int ClassroomSize { get; set; }
    }
}
