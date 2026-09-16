using System;
using System.Collections.Generic;
using System.Linq;
using DemoProject.Books;

namespace DemoProject.EntityFrameworkCore.Seed.Host
{
    public class InitialBookCategoryCreator
    {
        private readonly DemoProjectDbContext _context;
        
        public InitialBookCategoryCreator(DemoProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateCategoriesAndBooks();
        }

        private void CreateCategoriesAndBooks()
        {
            // 1. Khai báo danh sách Category với ID cố định
            var catLiteratureId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var catEconomicsId  = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var catLifeSkillsId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var catITId         = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var catHistoryId    = Guid.Parse("55555555-5555-5555-5555-555555555555");

            if (!_context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Id = catLiteratureId, Name = "Văn học", IsDeleted = false },
                    new Category { Id = catEconomicsId,  Name = "Kinh tế", IsDeleted = false },
                    new Category { Id = catLifeSkillsId, Name = "Kỹ năng sống", IsDeleted = false },
                    new Category { Id = catITId,         Name = "Công nghệ thông tin", IsDeleted = false },
                    new Category { Id = catHistoryId,    Name = "Lịch sử", IsDeleted = false }
                };

                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }

            // 2. Khai báo danh sách 20 bản ghi Book
            if (!_context.Books.Any())
            {
                var now = DateTime.UtcNow;

                var books = new List<Book>
                {
                    // Văn học
                    new Book { Id = Guid.Parse("A1111111-0000-0000-0000-000000000001"), CategoryId = catLiteratureId, Title = "Tắt Đèn", Author = "Ngô Tất Tố", Price = 45000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("A1111111-0000-0000-0000-000000000002"), CategoryId = catLiteratureId, Title = "Số Đỏ", Author = "Vũ Trọng Phụng", Price = 55000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("A1111111-0000-0000-0000-000000000003"), CategoryId = catLiteratureId, Title = "Lão Hạc", Author = "Nam Cao", Price = 35000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("A1111111-0000-0000-0000-000000000004"), CategoryId = catLiteratureId, Title = "Chí Phèo", Author = "Nam Cao", Price = 40000.00m, IsDeleted = false, CreatedAt = now },

                    // Kinh tế
                    new Book { Id = Guid.Parse("B2222222-0000-0000-0000-000000000001"), CategoryId = catEconomicsId, Title = "Cha Giàu Cha Nghèo", Author = "Robert Kiyosaki", Price = 110000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("B2222222-0000-0000-0000-000000000002"), CategoryId = catEconomicsId, Title = "Nhà Đầu Tư Thông Thái", Author = "Benjamin Graham", Price = 180000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("B2222222-0000-0000-0000-000000000003"), CategoryId = catEconomicsId, Title = "Kinh Tế Học Vĩ Mô", Author = "N. Gregory Mankiw", Price = 250000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("B2222222-0000-0000-0000-000000000004"), CategoryId = catEconomicsId, Title = "Tư Duy Nhanh Và Chậm", Author = "Daniel Kahneman", Price = 195000.00m, IsDeleted = false, CreatedAt = now },

                    // Kỹ năng sống
                    new Book { Id = Guid.Parse("C3333333-0000-0000-0000-000000000001"), CategoryId = catLifeSkillsId, Title = "Đắc Nhân Tâm", Author = "Dale Carnegie", Price = 95000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("C3333333-0000-0000-0000-000000000002"), CategoryId = catLifeSkillsId, Title = "Quẳng Gánh Lo Đi Và Sống", Author = "Dale Carnegie", Price = 85000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("C3333333-0000-0000-0000-000000000003"), CategoryId = catLifeSkillsId, Title = "Tuổi Trẻ Đáng Giá Bao Nhiêu", Author = "Rosie Nguyễn", Price = 80000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("C3333333-0000-0000-0000-000000000004"), CategoryId = catLifeSkillsId, Title = "Thói Quản Nguyên Tử", Author = "James Clear", Price = 140000.00m, IsDeleted = false, CreatedAt = now },

                    // Công nghệ thông tin
                    new Book { Id = Guid.Parse("D4444444-0000-0000-0000-000000000001"), CategoryId = catITId, Title = "Clean Code", Author = "Robert C. Martin", Price = 350000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("D4444444-0000-0000-0000-000000000002"), CategoryId = catITId, Title = "Design Patterns", Author = "Erich Gamma", Price = 420000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("D4444444-0000-0000-0000-000000000003"), CategoryId = catITId, Title = "Refactoring", Author = "Martin Fowler", Price = 380000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("D4444444-0000-0000-0000-000000000004"), CategoryId = catITId, Title = "Head First Java", Author = "Kathy Sierra", Price = 290000.00m, IsDeleted = false, CreatedAt = now },

                    // Lịch sử
                    new Book { Id = Guid.Parse("E5555555-0000-0000-0000-000000000001"), CategoryId = catHistoryId, Title = "Sapiens: Lược Sử Loài Người", Author = "Yuval Noah Harari", Price = 210000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("E5555555-0000-0000-0000-000000000002"), CategoryId = catHistoryId, Title = "Việt Nam Sử Lược", Author = "Trần Trọng Kim", Price = 160000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("E5555555-0000-0000-0000-000000000003"), CategoryId = catHistoryId, Title = "Súng, Vi Trùng Và Thép", Author = "Jared Diamond", Price = 230000.00m, IsDeleted = false, CreatedAt = now },
                    new Book { Id = Guid.Parse("E5555555-0000-0000-0000-000000000004"), CategoryId = catHistoryId, Title = "Đại Việt Sử Ký Toàn Thư", Author = "Nhiều tác giả", Price = 320000.00m, IsDeleted = false, CreatedAt = now }
                };

                _context.Books.AddRange(books);
                _context.SaveChanges();
            }
        }
    }
}