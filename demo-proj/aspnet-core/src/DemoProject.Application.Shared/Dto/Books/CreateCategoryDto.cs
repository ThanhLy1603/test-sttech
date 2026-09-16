using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DemoProject.Books;

namespace DemoProject.Application.Shared.Dto.Books
{
    [AutoMapTo(typeof(Category))]
    public class CreateCategoryDto
    {
        public string Name { get; set; }
    }
}