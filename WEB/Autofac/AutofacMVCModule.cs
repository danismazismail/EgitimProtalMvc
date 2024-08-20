using Autofac;
using AutoMapper;
using WEB.AutoMapper;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;
using DTO.Abstract;
using Business.Managers.Concrete;
using Business.Managers.Interface;
using WEB.Areas.Admin.Models.CustomerManagers;
using DTO.Concrete.CustomerManagers;
using Business.AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using WEB.ActionFilters;

namespace WEB.Autofac
{
    public class AutofacMVCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseRepository<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(BaseManager<,>).Assembly).AsClosedTypesOf(typeof(IBaseManager<,>)).InstancePerLifetimeScope();

            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();


            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<ValidateTokenExpiryFilter>();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ClassroomMapping());
                mc.AddProfile(new TeacherMapping());
                mc.AddProfile(new StudentMapping());
                mc.AddProfile(new CustomerManagerMapping());
                mc.AddProfile(new RoleMapping());
                mc.AddProfile(new UserMapping());

                mc.AddProfile(new ClassroomBusinessMapping());
                mc.AddProfile(new TeacherBusinessMapping());
                mc.AddProfile(new StudentBusinessMapping());
                mc.AddProfile(new CustomerManagerBusinessMapping());
                mc.AddProfile(new RoleBusinessMapping());
                mc.AddProfile(new UserBusinessMapping());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance<IMapper>(mapper);


        }
    }
}
