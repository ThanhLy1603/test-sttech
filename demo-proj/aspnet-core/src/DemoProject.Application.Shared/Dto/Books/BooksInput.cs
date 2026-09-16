using Abp.Application.Services.Dto;

namespace DemoProject.Application.Shared.Dto.Books
{
    public class BooksInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}