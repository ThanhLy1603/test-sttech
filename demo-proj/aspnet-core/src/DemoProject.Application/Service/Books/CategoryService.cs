using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DemoProject.Application.Shared.Dto.Books;
using DemoProject.Application.Shared.Interface.Books;
using DemoProject.Books;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Runtime.Validation;

namespace DemoProject.Service.Books
{
    [RemoteService(IsEnabled = true)]
    [DisableValidation]
    public class CategoryService : ApplicationService, ICategoryService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryService(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllListAsync();

            return ObjectMapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<PagedResultDto<CategoryDto>> GetAllAsync(CategoriesInput input)
        {
            // Gán giá trị an toàn nếu client không truyền lên
            input ??= new CategoriesInput();
            if (input.MaxResultCount <= 0) input.MaxResultCount = 10;

            var query = _categoryRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), category => category.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();

            // Sắp xếp
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderBy(c => c.Name);
            }

            // Phân trang bằng LINQ thuần (Không phụ thuộc ABP)
            var categories = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var categoryDtos = ObjectMapper.Map<List<CategoryDto>>(categories);

            return new PagedResultDto<CategoryDto>(totalCount, categoryDtos);
        }

        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);

            return ObjectMapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(
            CreateCategoryDto createCategoryDto)
        {
            createCategoryDto.Name = createCategoryDto.Name?.Trim();

            if (string.IsNullOrWhiteSpace(createCategoryDto.Name))
            {
                throw new UserFriendlyException("Tên danh mục không được để trống");
            }
            
            var isNameExist =
                await _categoryRepository.FirstOrDefaultAsync(
                    category => category.Name == createCategoryDto.Name
                );

            if (isNameExist != null)
            {
                throw new UserFriendlyException($"Tên danh mục '{createCategoryDto.Name}' đã tồn tại trong hệ thống.");
            }

            var category = ObjectMapper.Map<Category>(createCategoryDto);

            await _categoryRepository.InsertAsync(category);

            return ObjectMapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(
            UpdateCategoryDto updateCategoryDto)
        {
            updateCategoryDto.Name = updateCategoryDto.Name.Trim();

            if (string.IsNullOrWhiteSpace(updateCategoryDto.Name))
            {
                throw new UserFriendlyException("Tên danh mục không được để trống.");
            }
            
            var category =
                await _categoryRepository.GetAsync(updateCategoryDto.Id);

            var isNameExist =
                await _categoryRepository.FirstOrDefaultAsync(
                    category =>
                        category.Name == updateCategoryDto.Name &&
                        category.Id != updateCategoryDto.Id
                );

            if (isNameExist != null)
            {
                throw new UserFriendlyException($"Tên danh mục '{updateCategoryDto.Name}' đã tồn tại trong hệ thống.");
            }

            ObjectMapper.Map(updateCategoryDto, category);

            await _categoryRepository.UpdateAsync(category);

            return ObjectMapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            // ABP sẽ tự động Soft Delete nếu Entity hỗ trợ ISoftDelete
            await _categoryRepository.DeleteAsync(id);
        }
    }
}