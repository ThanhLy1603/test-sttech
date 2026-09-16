using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace DemoProject.Books
{
    [Table("ST_Category")]
    public class Category : Entity<Guid>, ISoftDelete
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}