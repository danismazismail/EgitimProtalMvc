using ApplicationCore.Entities.UserEntites.Concrete;
using AutoMapper;
using DTO.Concrete.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class UserBusinessMapping : Profile
    {
        public UserBusinessMapping()
        {
            CreateMap<AppUser, GetUserDTO>().ReverseMap();
            CreateMap<AppUser, GetAppUserDTO>().ReverseMap();
            CreateMap<AppUser, CreateUserDTO>().ReverseMap();
            CreateMap<AppUser, GetUserForRoleDTO>().ReverseMap();
            CreateMap<EditUserDTO, AppUser>()
                 .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore()).ReverseMap();
        }
    }
}
