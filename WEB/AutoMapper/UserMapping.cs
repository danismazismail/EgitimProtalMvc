using AutoMapper;
using DTO.Concrete.Students;
using DTO.Concrete.Users;
using WEB.Areas.Admin.Models.CustomerManagers;
using WEB.Areas.Admin.Models.Users;
using WEB.Areas.Education.Models.ViewModels.Students;
using WEB.Areas.Education.Models.ViewModels.Teachers;
using WEB.Models.ViewModels.Users;

namespace WEB.AutoMapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<GetUserForRoleDTO, GetUserForRoleVM>().ReverseMap();
            
            CreateMap<LoginDTO, LoginVM>().ReverseMap();
            CreateMap<ResetPasswordDTO, ResetPasswordVM>().ReverseMap();
            CreateMap<CreatePasswordDTO, CreatePasswordVM>().ReverseMap();
            CreateMap<ChangePasswordDTO, ChangePasswordVM>().ReverseMap();

            CreateMap<EditUserVM, EditUserDTO>().ReverseMap();
            
            CreateMap<CreateUserDTO, CreateTeacherVM>().ReverseMap();
            CreateMap<CreateUserDTO, CreateStudentVM>().ReverseMap();
            CreateMap<CreateUserDTO, CreateCMVM>().ReverseMap();


        }
    }
}
