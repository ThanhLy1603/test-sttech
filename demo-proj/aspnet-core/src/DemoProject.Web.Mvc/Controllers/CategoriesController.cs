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
    public class CategoriesController : AbpController
    {
        private readonly ICategoryService _categoryService;
        
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoriesInput input)
        {
            input ??= new CategoriesInput();
            
            var result = await _categoryService.GetAllAsync(input);
            
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryService.CreateAsync(createCategoryDto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> EditModal(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return PartialView("_EditModal", category);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryService.UpdateAsync(updateCategoryDto);
            return Json(result);
        }
    
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            
            return Json(new { success = true });
        }
    }
}
