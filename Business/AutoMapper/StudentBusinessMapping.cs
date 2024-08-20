using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class StudentBusinessMapping : Profile
    {
        public StudentBusinessMapping()
        {
            CreateMap<GetStudentDTO, Student>().ReverseMap();
            CreateMap<CreateStudentDTO, Student>().ReverseMap();
            CreateMap<StudentDetailForExamsDTO, Student>().ReverseMap();
            CreateMap<StudentDetailForProjectDTO, Student>().ReverseMap();
            CreateMap<UpdateStudentDTO, Student>()
                 .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore()).ReverseMap();
        }
    }
}
