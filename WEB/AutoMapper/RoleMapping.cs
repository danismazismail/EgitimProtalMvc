using AutoMapper;
using DTO.Concrete.Roles;
using WEB.Areas.Admin.Models.Roles;

namespace WEB.AutoMapper
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<GetRoleDTO, GetRoleVM>()
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(x => x.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ReverseMap();
            CreateMap<CreateRoleVM, CreateRoleDTO>().ReverseMap();

            CreateMap<UpdateRoleDTO, UpdateRoleVM>().ReverseMap();
        }
    }
}
