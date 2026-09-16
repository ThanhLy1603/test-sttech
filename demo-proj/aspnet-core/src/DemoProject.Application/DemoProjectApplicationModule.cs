using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DemoProject.Application.Shared.Dto.Books;
using DemoProject.Authorization;
using DemoProject.Books;

namespace DemoProject;

[DependsOn(
    typeof(DemoProjectCoreModule),
    typeof(AbpAutoMapperModule))]
public class DemoProjectApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<DemoProjectAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(DemoProjectApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            cfg =>
            {
                // 1. Quét các Profile trong Assembly Application
                cfg.AddMaps(thisAssembly);

                // 2. Đăng ký thủ công Mapping giữa Category và CategoryDto
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<CreateCategoryDto, Category>();
                cfg.CreateMap<UpdateCategoryDto, Category>();
                
                // 2. Đăng ký thủ công Mapping giữa Book và BookDto
                cfg.CreateMap<Book, BookDto>();
                cfg.CreateMap<BookDto, Book>();
                cfg.CreateMap<CreateBookDto, Book>();
                cfg.CreateMap<UpdateBookDto, Book>();
            }
        );
    }
}   
