using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace DemoProject.Web.Views;

public abstract class DemoProjectRazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected DemoProjectRazorPage()
    {
        LocalizationSourceName = DemoProjectConsts.LocalizationSourceName;
    }
}
