using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DataAccess.Services.Interface;
using DTO.Abstract;
using DTO.Concrete.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class TeacherManager : BaseManager<ITeacherService, Teacher>, ITeacherManager
    {
        public TeacherManager(ITeacherService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
