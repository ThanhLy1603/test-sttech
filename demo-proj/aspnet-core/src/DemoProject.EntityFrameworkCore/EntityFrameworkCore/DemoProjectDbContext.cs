using Abp.Zero.EntityFrameworkCore;
using DemoProject.Authorization.Roles;
using DemoProject.Authorization.Users;
using DemoProject.Books;
using DemoProject.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.EntityFrameworkCore;

public class DemoProjectDbContext : AbpZeroDbContext<Tenant, Role, User, DemoProjectDbContext>
{
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Book> Books { get; set; }

    public DemoProjectDbContext(DbContextOptions<DemoProjectDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Category>(b =>
        {
            b.ToTable("ST_Category");
            
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100); 

            b.Property(x => x.IsDeleted)
                .HasColumnName("Is_Deleted")
                .HasDefaultValue(false); 
        });
        
        modelBuilder.Entity<Book>(b =>
        {
            b.ToTable("ST_Book", t => 
                t.HasCheckConstraint("CK_ST_Book_Price", "[Price] >= 0")); // CHECK (Price >= 0)

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property(x => x.CategoryId)
                .HasColumnName("Category_Id")
                .IsRequired();

            b.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100); 

            b.Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(100); 

            b.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            b.Property(x => x.IsDeleted)
                .HasColumnName("Is_Deleted")
                .HasDefaultValue(false); 

            b.Property(x => x.CreatedAt)
                .HasColumnName("Created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()"); 

            
            b.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
