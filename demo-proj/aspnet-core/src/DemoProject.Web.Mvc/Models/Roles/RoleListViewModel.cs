using DemoProject.Roles.Dto;
using System.Collections.Generic;

namespace DemoProject.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
