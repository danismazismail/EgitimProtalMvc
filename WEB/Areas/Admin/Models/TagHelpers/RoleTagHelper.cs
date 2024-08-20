using Business.Managers.Interface;
using DTO.Concrete.Roles;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WEB.Areas.Admin.Models.Roles;

namespace WEB.Areas.Admin.Models.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class RoleTagHelper : TagHelper
    {
        private readonly IRoleManager _roleManager;
        private readonly IUserManager _userManager;

        //TODO
        public RoleTagHelper(IRoleManager roleManager, IUserManager userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("user-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> userNames = new List<string>();
            var role = await _roleManager.FindRoleAsync<GetRoleDTO>(Guid.Parse(RoleId));

            if (role != null)
            {
                var users = await _userManager.GetUsers();
                foreach ( var user in users )
                {
                    if (await _userManager.IsUserInRole(user, role.Name))
                    {
                        userNames.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent
                (
                    userNames.Count == 0 ? "Bu rolde hiçbir kullanıcı yok!" :
                    userNames.Count > 3 ? 
                    (string.Join(", ", userNames.Take(3)) + " ...")
                    : string.Join(", ", userNames)
                );
        }
    }
}
