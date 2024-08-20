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
    public class ClassroomManager : BaseManager<IClassroomService, Classroom>, IClassroomManager
    {
        public ClassroomManager(IClassroomService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
