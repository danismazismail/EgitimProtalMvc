using ApplicationCore.Entities.Concrete;
using AutoMapper;
using Business.Managers.Interface;
using DataAccess.Services.Interface;
using DTO.Abstract;
using DTO.Concrete;
using DTO.Concrete.CustomerManagers;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class CMManager : BaseManager<ICustomerManagerService, CustomerManager>, ICMManager
    {
        public CMManager(ICustomerManagerService service, IMapper mapper) : base(service, mapper)
        {
        }

    }
}
