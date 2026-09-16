using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DemoProject.Books;

namespace DemoProject.Application.Shared.Dto.Books
{
    [AutoMapTo(typeof(Book))]
    public class UpdateBookDto
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public Guid CategoryId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}