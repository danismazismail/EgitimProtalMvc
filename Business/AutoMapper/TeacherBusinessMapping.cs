using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class TeacherBusinessMapping : Profile
    {
        public TeacherBusinessMapping()
        {
            CreateMap<CreateTeacherDTO, Teacher>().ReverseMap();
            CreateMap<UpdateTeacherDTO, Teacher>()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore()).ReverseMap();
            CreateMap<GetTeacherDTO, Teacher>().ReverseMap();
        }
    }
}
