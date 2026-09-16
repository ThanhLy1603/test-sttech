using DemoProject.Roles.Dto;
using System.Collections.Generic;

namespace DemoProject.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}