using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DemoProject.Books;

namespace DemoProject.Application.Shared.Dto.Books
{
    [AutoMapTo(typeof(Book))]
    public class CreateBookDto
    {
        [Required(ErrorMessage = "Vui lòng chọn dạnh mục")]
        public Guid CategoryId { get; set; }
        
        [Required(ErrorMessage = "Tewen sách không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sách không được vượt quá {1} ký tự")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Tên tác giả không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên tác giả không được vượt quá {1} ký tự.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sách.")]
        [Range(0, 999999999.99, ErrorMessage = "Giá sách phải nằm trong khoảng từ 0 đến 999,999,999 đ.")]
        public decimal Price { get; set; }
    }
}