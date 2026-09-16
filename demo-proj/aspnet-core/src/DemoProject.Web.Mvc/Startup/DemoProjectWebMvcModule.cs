using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DemoProject.Configuration;
using DemoProject.EntityFrameworkCore;
using DemoProject.EntityFrameworkCore.Seed.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace DemoProject.Web.Startup;

[DependsOn(typeof(DemoProjectWebCoreModule))]
public class DemoProjectWebMvcModule : AbpModule
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfigurationRoot _appConfiguration;

    public DemoProjectWebMvcModule(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public override void PreInitialize()
    {
        Configuration.Navigation.Providers.Add<DemoProjectNavigationProvider>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(DemoProjectWebMvcModule).GetAssembly());
    }
    
    // public override void PostInitialize()
    // {
    //     // Chỉ làm nhiệm vụ nạp Seed Data
    //     var workManager = IocManager.Resolve<IUnitOfWorkManager>();
    //     using (var uow = workManager.Begin())
    //     {
    //         var dbContextProvider = IocManager.Resolve<IDbContextProvider<DemoProjectDbContext>>();
    //         var dbContext = dbContextProvider.GetDbContext();
    //
    //         new InitialBookCategoryCreator(dbContext).Create();
    //
    //         uow.Complete();
    //     }
    // }
}
