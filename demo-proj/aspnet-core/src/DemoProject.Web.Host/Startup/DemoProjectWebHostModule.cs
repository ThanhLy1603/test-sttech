using Abp.Modules;
using Abp.Reflection.Extensions;
using DemoProject.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DemoProject.Web.Host.Startup
{
    [DependsOn(
       typeof(DemoProjectWebCoreModule))]
    public class DemoProjectWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DemoProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoProjectWebHostModule).GetAssembly());
        }
    }
}
