using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DemoProject.Books;

namespace DemoProject.Application.Shared.Dto.Books
{
    [AutoMapTo(typeof(Book))]
    public class BookDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Price { get; set; }
        public CategoryDto Category { get; set; }
    }
}