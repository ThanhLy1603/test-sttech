using Abp.Authorization;
using Abp.Runtime.Session;
using DemoProject.Configuration.Dto;
using System.Threading.Tasks;

namespace DemoProject.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : DemoProjectAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
