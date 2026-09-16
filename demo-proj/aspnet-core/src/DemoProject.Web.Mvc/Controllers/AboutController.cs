using Abp.AspNetCore.Mvc.Authorization;
using DemoProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Web.Controllers;

[AbpMvcAuthorize]
public class AboutController : DemoProjectControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
