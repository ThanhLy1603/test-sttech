using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace DemoProject.Controllers
{
    public abstract class DemoProjectControllerBase : AbpController
    {
        protected DemoProjectControllerBase()
        {
            LocalizationSourceName = DemoProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
