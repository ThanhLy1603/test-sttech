using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DemoProject.Application.Shared.Dto.Books;

namespace DemoProject.Application.Shared.Interface.Books;

public interface IBookService : IApplicationService
{
    Task<PagedResultDto<BookDto>> GetAllAsync(BooksInput input);
    
    Task<BookDto> GetByIdAsync(Guid id);
    
    Task<BookDto> CreateAsync(CreateBookDto createBookDto);
    
    Task<BookDto> UpdateAsync(UpdateBookDto updateBookDto);
    
    Task DeleteAsync(Guid id);
}