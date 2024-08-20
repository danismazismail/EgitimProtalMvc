using Business.Managers.Interface;
using DTO.Concrete.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB.ActionFilters
{
    public class ValidateTokenExpiryFilter : ActionFilterAttribute, IAsyncActionFilter
    {

        private IUserManager _userManager;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _userManager = context.HttpContext.RequestServices.GetService<IUserManager>();

            // Action metodundan önce çalışacak kodlar
            if (context.ActionArguments.TryGetValue("token", out var tokenValue) && context.ActionArguments.TryGetValue("email", out var emailValue))
            {
                string email = emailValue as string;
                string token = tokenValue as string;

                var user = await _userManager.FindUser<GetUserDTO>(email);

                if (user != null)
                {
                    var tokenIsValid = await _userManager.TokenIsValid(user, token);
                    if (tokenIsValid)
                    {
                        await next();
                        return;
                    }

                    context.Result = new BadRequestObjectResult("Bu bağlantının süresi geçmiş!");
                }
            }

        }
    }
}
