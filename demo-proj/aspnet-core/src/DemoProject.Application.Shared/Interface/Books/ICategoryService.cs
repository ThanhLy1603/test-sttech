using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DemoProject.Application.Shared.Dto.Books;

namespace DemoProject.Application.Shared.Interface.Books
{
    public interface ICategoryService : IApplicationService
    {
        Task<PagedResultDto<CategoryDto>> GetAllAsync(
            CategoriesInput input);

        Task<CategoryDto> GetByIdAsync(Guid id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto input);

        Task<CategoryDto> UpdateAsync(UpdateCategoryDto input);

        Task DeleteAsync(Guid id);
    }
}