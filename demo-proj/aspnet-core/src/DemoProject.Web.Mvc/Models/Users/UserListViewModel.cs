using DemoProject.Roles.Dto;
using System.Collections.Generic;

namespace DemoProject.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
