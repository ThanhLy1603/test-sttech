using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using DemoProject.Application.Shared.Dto.Books;
using DemoProject.Application.Shared.Interface.Books;
using DemoProject.Controllers;
using DemoProject.Service.Books;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : DemoProjectControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IBookService _bookService;

    public HomeController(ICategoryService categoryService, BookService bookService)
    {
        _categoryService = categoryService;
        _bookService = bookService;
    }
    
    public async Task<ActionResult> Index()
    {
        var result = await _categoryService.GetAllAsync(new CategoriesInput
        {
            MaxResultCount = 10,
            SkipCount = 0
        });
    
        Console.WriteLine("=================== DANH SÁCH DANH MỤC ===================");
        foreach (var category in result.Items)
        {
            Console.WriteLine($"ID: {category.Id} | Tên danh mục: {category.Name}");
        }
        Console.WriteLine("==========================================================");

        var categoryById = await _categoryService.GetByIdAsync(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        
        Console.WriteLine("=================== DANH MỤC THEO ID ===================");
        Console.WriteLine($"ID: {categoryById.Id} | Tên danh mục: {categoryById.Name}");

        var books = await _bookService.GetAllAsync(new BooksInput
        {
            MaxResultCount = 10,
            SkipCount = 0
        });
        
        Console.WriteLine("=================== DANH SÁCH SÁCH ===================");

        foreach (var book in books.Items)
        {
            // Lấy tên danh mục (xử lý null nếu sách chưa có Category)
            var categoryName = book.Category != null ? book.Category.Name : "N/A";

            Console.WriteLine($"Tên sách: {book.Title} | Tác giả: {book.Author} | Giá: {book.Price} | Danh mục: {categoryName}");
        }

        Console.WriteLine("======================================================");
        
        Console.WriteLine("=================== DANH MỤC THEO ID ===================");
        var bookById = await _bookService.GetByIdAsync(Guid.Parse("b2222222-0000-0000-0000-000000000001"));
        var categoryNameByBookId = bookById.Category != null ? bookById.Category.Name : "N/A";

        Console.WriteLine($"Tên sách: {bookById.Title} | Tác giả: {bookById.Author} | Giá: {bookById.Price} | Danh mục: {categoryNameByBookId}");
    
        return View();
    }
}
