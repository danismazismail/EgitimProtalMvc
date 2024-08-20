using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DataAccess.Services.Interface;
using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class StudentManager : BaseManager<IStudentService, Student>, IStudentManager
    {
        public StudentManager(IStudentService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
