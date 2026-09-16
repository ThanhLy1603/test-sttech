using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using DemoProject.Application.Shared.Dto.Books;
using DemoProject.Application.Shared.Interface.Books;
using DemoProject.Books;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Service.Books
{
    public class BookService : ApplicationService, IBookService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;

        public BookService(
            IRepository<Book, Guid> bookRepository,
            IRepository<Category, Guid> categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }
        
        public async Task<PagedResultDto<BookDto>> GetAllAsync(BooksInput input)
        {
            var query = _bookRepository.GetAll()
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    book => book.Title.Contains(input.Filter) ||
                            book.Author.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            
            var books = await query
                .PageBy(input)
                .ToListAsync();
            
            var bookDtos = ObjectMapper.Map<List<BookDto>>(books);

            return new PagedResultDto<BookDto>(totalCount, bookDtos);
        }

        public async Task<BookDto> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return ObjectMapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto createBookDto)
        {
            var isCategoryExist = await _categoryRepository.FirstOrDefaultAsync(c => c.Id == createBookDto.CategoryId);
            if (isCategoryExist == null)
            {
                throw new UserFriendlyException("Danh mục lựa chọn không tồn tại");
            }
            
            var book = ObjectMapper.Map<Book>(createBookDto);
            
            book.CreatedAt = DateTime.Now;
            await _bookRepository.InsertAsync(book);
            
            return ObjectMapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateAsync(UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetAsync(updateBookDto.Id);
            
            var isCategoryExist = await _categoryRepository.FirstOrDefaultAsync(c => c.Id == updateBookDto.CategoryId);
            if (isCategoryExist == null)
            {
                throw new UserFriendlyException("Danh mục lựa chọn không tồn tại");
            }
            
            ObjectMapper.Map(updateBookDto, book);
            
            await _bookRepository.UpdateAsync(book);
            
            return ObjectMapper.Map<BookDto>(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}

