using FluentValidation;
using FluentValidation.AspNetCore;
using WEB.FluentValidation.ClasroomValidation;
using WEB.FluentValidation.CustomerManagerValidation;
using WEB.FluentValidation.RoleValidation;
using WEB.FluentValidation.StudentValidation;
using WEB.FluentValidation.TeacherValidation;
using WEB.FluentValidation.UserValidation;

namespace WEB.ServiceExtensions
{
    public static class ValidatorServiceRegistration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCMValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCMValidator>();
            
            services.AddValidatorsFromAssemblyContaining<CreateTeacherValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateTeacherValidator>();
           
            services.AddValidatorsFromAssemblyContaining<CreateClassroomValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateClassroomValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateStudentValidator>();
            services.AddValidatorsFromAssemblyContaining<EnterExamForStudentValidator>();
            services.AddValidatorsFromAssemblyContaining<SendProjectValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateRoleValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateRoleValidator>();

            services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            services.AddValidatorsFromAssemblyContaining<ForgotPasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<EditUserValidator>();
            services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>();


            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
