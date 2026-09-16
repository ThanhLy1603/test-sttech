using Abp.Application.Services;
using DemoProject.Sessions.Dto;
using System.Threading.Tasks;

namespace DemoProject.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
