using AutoMapper;
using DTO.Concrete.Clasrooms;
using WEB.Areas.Education.Models.ViewModels.Classrooms;

namespace WEB.AutoMapper
{
    public class ClassroomMapping : Profile
    {
        public ClassroomMapping()
        {
            CreateMap<CreateClassroomVM, CreateClassroomDTO>().ReverseMap();
            CreateMap<UpdateClassroomVM, UpdateClassroomDTO>().ReverseMap();
            CreateMap<GetClassroomsForTeacherVM, GetClassroomsForTeacherDTO>().ReverseMap();
        }
    }
}
