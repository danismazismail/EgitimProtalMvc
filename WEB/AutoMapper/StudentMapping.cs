using AutoMapper;
using DTO.Concrete.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Areas.Education.Models.ViewModels.Students;

namespace WEB.AutoMapper
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap<CreateStudentDTO, CreateStudentVM>().ReverseMap();
            CreateMap<UpdateStudentDTO, UpdateStudentVM>().ReverseMap();
            CreateMap<GetStudentDTO, GetStudentVM>().ReverseMap();
            CreateMap<GetStudentsForClassroomVM, GetStudentsForClassroomDTO>().ReverseMap();
            CreateMap<StudentDetailForExamsVM, StudentDetailForExamsDTO>().ReverseMap();
            CreateMap<StudentDetailForProjectVM, StudentDetailForProjectDTO>().ReverseMap();
        }
    }
}
