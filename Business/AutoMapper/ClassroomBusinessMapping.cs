using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.Clasrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class ClassroomBusinessMapping : Profile
    {
        public ClassroomBusinessMapping()
        {
            CreateMap<CreateClassroomDTO, Classroom>().ReverseMap();
            CreateMap<GetClassroomsForTeacherDTO, Classroom>().ReverseMap();
            CreateMap<GetClassroomDTO, Classroom>().ReverseMap();
            
            CreateMap<UpdateClassroomDTO, Classroom>()
                 .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore()).ReverseMap();
        }
    }
}
