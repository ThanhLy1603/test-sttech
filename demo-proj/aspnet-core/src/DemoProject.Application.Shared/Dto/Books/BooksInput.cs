using Abp.Application.Services.Dto;

namespace DemoProject.Application.Shared.Dto.Books
{
    public class BooksInput
    {
        public string Filter { get; set; }
        public int SkipCount { get; set; } = 0;
        public int MaxResultCount { get; set; } = 10;
        public string Sorting { get; set; } = "Title ASC";
    }
}