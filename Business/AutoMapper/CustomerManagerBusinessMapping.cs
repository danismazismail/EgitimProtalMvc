using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DTO.Concrete.CustomerManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class CustomerManagerBusinessMapping : Profile
    {
        public CustomerManagerBusinessMapping()
        {
            CreateMap<CreateCMDTO, CustomerManager>().ReverseMap();
            CreateMap<UpdateCMDTO, CustomerManager>()
                 .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore()).ReverseMap();
            CreateMap<GetCMDTO, CustomerManager>().ReverseMap();
        }
    }
}
