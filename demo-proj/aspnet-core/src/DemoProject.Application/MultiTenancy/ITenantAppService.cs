using Abp.Application.Services;
using DemoProject.MultiTenancy.Dto;

namespace DemoProject.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

