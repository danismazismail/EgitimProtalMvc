using AutoMapper;
using DTO.Concrete.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Areas.Education.Models.ViewModels.Teachers;

namespace WEB.AutoMapper
{
    public class TeacherMapping : Profile
    {
        public TeacherMapping()
        {
            CreateMap<CreateTeacherVM, CreateTeacherDTO>().ReverseMap();
            CreateMap<UpdateTeacherVM, UpdateTeacherDTO>().ReverseMap();
        }
    }
}
