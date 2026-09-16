using Abp.AspNetCore.Mvc.ViewComponents;

namespace DemoProject.Web.Views;

public abstract class DemoProjectViewComponent : AbpViewComponent
{
    protected DemoProjectViewComponent()
    {
        LocalizationSourceName = DemoProjectConsts.LocalizationSourceName;
    }
}
