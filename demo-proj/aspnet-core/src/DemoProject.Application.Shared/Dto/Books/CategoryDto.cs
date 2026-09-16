using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DemoProject.Books;

namespace DemoProject.Application.Shared.Dto.Books
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}

