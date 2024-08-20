using AutoMapper;
using DTO.Concrete.CustomerManagers;
using WEB.Areas.Admin.Models.CustomerManagers;

namespace WEB.AutoMapper
{
    public class CustomerManagerMapping : Profile
    {
        public CustomerManagerMapping()
        {
            CreateMap<CreateCMVM, CreateCMDTO>().ReverseMap();
            CreateMap<UpdateCMVM, UpdateCMDTO>().ReverseMap();
        }
    }
}
