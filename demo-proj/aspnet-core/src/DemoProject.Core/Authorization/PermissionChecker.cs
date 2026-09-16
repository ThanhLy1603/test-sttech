using Abp.Authorization;
using DemoProject.Authorization.Roles;
using DemoProject.Authorization.Users;

namespace DemoProject.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
