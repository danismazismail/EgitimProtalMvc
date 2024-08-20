using ApplicationCore.Entities.UserEntites.Concrete;
using AutoMapper;
using DTO.Concrete.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class RoleBusinessMapping : Profile
    {
        public RoleBusinessMapping()
        {
            CreateMap<AppRole, CreateRoleDTO>().ReverseMap();
            CreateMap<UpdateRoleDTO, AppRole>()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore()).ReverseMap();
            CreateMap<AppRole, GetRoleDTO>().ReverseMap();
        }
    }
}
