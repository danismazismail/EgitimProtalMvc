using ApplicationCore.Entities.Concrete;
using DataAccess.Services.Interface;
using DTO.Abstract;
using DTO.Concrete.CustomerManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Interface
{
    public interface ICMManager: IBaseManager<ICustomerManagerService, CustomerManager>
    {
    }
}
