using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Runtime.Validation;
using DemoProject.Application.Shared.Dto.Books;
using DemoProject.Application.Shared.Interface.Books;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Web.Controllers
{
    [DisableValidation]
    public class BooksController : AbpController
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        
        public BooksController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BooksInput input)
        {
            input ??= new BooksInput();
            
            var books = await _bookService.GetAllAsync(input);

            return Json(books);
        }

        [HttpGet]
        public async Task<IActionResult> EditModal(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            
            return PartialView("_EditModal", book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto)
        {
            var result = await _bookService.CreateAsync(createBookDto);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto updateBookDto)
        {
            var result = await _bookService.UpdateAsync(updateBookDto);
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            
            return Json(new { success = true });
        }
    }
}