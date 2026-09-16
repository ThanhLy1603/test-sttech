using DemoProject.Configuration.Dto;
using System.Threading.Tasks;

namespace DemoProject.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
