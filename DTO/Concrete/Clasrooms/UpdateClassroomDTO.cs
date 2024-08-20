using DTO.Abstract;

namespace DTO.Concrete.Clasrooms
{
    public class UpdateClassroomDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
    }
}
