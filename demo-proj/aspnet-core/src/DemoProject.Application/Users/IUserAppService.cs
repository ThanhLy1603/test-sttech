using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DemoProject.Roles.Dto;
using DemoProject.Users.Dto;
using System.Threading.Tasks;

namespace DemoProject.Users;

public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
{
    Task DeActivate(EntityDto<long> user);
    Task Activate(EntityDto<long> user);
    Task<ListResultDto<RoleDto>> GetRoles();
    Task ChangeLanguage(ChangeUserLanguageDto input);

    Task<bool> ChangePassword(ChangePasswordDto input);
}
