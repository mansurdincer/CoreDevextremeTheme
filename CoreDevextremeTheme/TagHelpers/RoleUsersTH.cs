using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreDevextremeTheme.TagHelpers;
[HtmlTargetElement("td", Attributes = "i-role")]
public class RoleUsersTH : TagHelper
{
    private UserManager<ApplicationUser> userManager;
    private RoleManager<IdentityRole> roleManager;

    public RoleUsersTH(UserManager<ApplicationUser> usermgr, RoleManager<IdentityRole> rolemgr)
    {
        userManager = usermgr;
        roleManager = rolemgr;
    }

    [HtmlAttributeName("i-role")]
    public string Role { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        List<string> names = new List<string>();

        IdentityRole role = await roleManager.FindByIdAsync(Role);

        if (role != null)
        {
            var users = await userManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in userManager.Users)
            {
                if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    names.Add(user.UserName);
            }
        }

        //fake data
        //names.AddRange(new string[] { "Mansur", "Akın"});

        output.Content.SetContent(names.Count == 0 ? "" : string.Join(", ", names));
    }
}
