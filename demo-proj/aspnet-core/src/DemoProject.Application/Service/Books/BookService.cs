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
using System.Linq.Dynamic.Core;

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
            input ??= new BooksInput();

            var query = _bookRepository.GetAll()
                .Include(book => book.Category)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    book => book.Title.Contains(input.Filter) ||
                            book.Author.Contains(input.Filter));

            var totalCount = await query.CountAsync();

            // Sắp xếp linh hoạt theo chuỗi (Cần System.Linq.Dynamic.Core)
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderBy("Title ASC");
            }

            // Phân trang bằng PageBy truyền tham số trực tiếp từ DTO của bạn
            var books = await query
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToListAsync();

            var bookDtos = ObjectMapper.Map<List<BookDto>>(books);

            return new PagedResultDto<BookDto>(totalCount, bookDtos);
        }

        public async Task<BookDto> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetAllIncluding(book => book.Category)
                .FirstOrDefaultAsync(book => book.Id == id);
            
            if (book == null)
            {
                throw new UserFriendlyException("Không tìm thấy sách!");
            }
            
            return ObjectMapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto createBookDto)
        {
            createBookDto.Title = createBookDto.Title?.Trim();
            createBookDto.Author = createBookDto.Author?.Trim();
            
            var isCategoryExist = await _categoryRepository.FirstOrDefaultAsync(category => category.Id == createBookDto.CategoryId);
            if (isCategoryExist == null)
            {
                throw new UserFriendlyException("Danh mục lựa chọn không tồn tại");
            }
            
            var isTitleExist = await _bookRepository.FirstOrDefaultAsync(book => book.Title == createBookDto.Title);
            if (isTitleExist != null)
            {
                throw new UserFriendlyException($"Tên sách '{createBookDto.Title}' đã tồn tại trong hệ thống.");
            }
            
            var book = ObjectMapper.Map<Book>(createBookDto);
            
            book.CreatedAt = DateTime.Now;
            await _bookRepository.InsertAsync(book);
            
            return ObjectMapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateAsync(UpdateBookDto updateBookDto)
        {
            updateBookDto.Title = updateBookDto.Title?.Trim();
            updateBookDto.Author = updateBookDto.Author?.Trim();
            
            var book = await _bookRepository.GetAsync(updateBookDto.Id);
            
            var isCategoryExist = await _categoryRepository.FirstOrDefaultAsync(category => category.Id == updateBookDto.CategoryId);
            if (isCategoryExist == null)
            {
                throw new UserFriendlyException("Danh mục lựa chọn không tồn tại");
            }
            
            var isTitleExist = await _bookRepository.FirstOrDefaultAsync(book => 
                book.Title.ToLower() == updateBookDto.Title.ToLower() && book.Id != updateBookDto.Id);

            if (isTitleExist != null)
            {
                throw new UserFriendlyException($"Tên sách '{updateBookDto.Title}' đã tồn tại trong hệ thống.");
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