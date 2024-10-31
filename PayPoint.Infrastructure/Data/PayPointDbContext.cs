using Microsoft.EntityFrameworkCore;
using PayPoint.Core.Entities;
using PayPoint.Infrastructure.Data.Interceptors;

namespace PayPoint.Infrastructure.Data;

public class PayPointDbContext : DbContext
{
    public PayPointDbContext(DbContextOptions<PayPointDbContext> options)
        : base(options) { }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CategoryEntity> SubCategories { get; set; }
    public DbSet<ProductIngredientEntity> ProductIngredients { get; set; }
    public DbSet<IngredientEntity> Ingredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Products relationship.
        modelBuilder.Entity<ProductEntity>(b =>
        {
            b.HasKey(e => e.ProductId);
            
            b.Property(e => e.ProductId).ValueGeneratedOnAdd();
            
            b.Property(e => e.Price)
             .HasPrecision(12, 2)
             .IsRequired();
            
            b.HasOne(e => e.SubCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(e => e.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            
            b.HasMany(e => e.ProductIngredients)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Products");
        });

        // Categories relationship.
        modelBuilder.Entity<CategoryEntity>(b =>
        {
            b.HasKey(x => x.CategoryId);

            b.Property(x => x.CategoryId).ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(75)
                .IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(150)
                .HasDefaultValue(string.Empty);

            b.HasMany(x => x.SubCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            b.ToTable("Categories");
            
        });

        // SubCategories relationship.
        modelBuilder.Entity<SubCategoryEntity>(b =>
        {
            b.HasKey(x => x.SubCategoryId);

            b.Property(x => x.SubCategoryId).ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(75)
                .IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(150)
                .HasDefaultValue(string.Empty);
            b.ToTable("SubCategories");
        });

        // Ingredients relationship.
        modelBuilder.Entity<IngredientEntity>(b =>
        {
            b.HasKey(x => x.IngredientId);

            b.Property(x => x.IngredientId).ValueGeneratedOnAdd();

            b.Property(x => x.Name)
                .HasMaxLength(75)
                .IsRequired();

            b.Property(x => x.AdditionalPrice)
                .HasPrecision(12, 2);

            b.HasMany(x => x.ProductIngredients)
                .WithOne(x => x.Ingredient)
                .HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.NoAction);    
            b.ToTable("Ingredients");
        });

        // Products Ingredients relationship.
        modelBuilder.Entity<ProductIngredientEntity>(b =>
        {
            b.HasKey(x => new { x.ProductId, x.IngredientId });

            b.HasOne(x => x.Product)
                .WithMany(x => x.ProductIngredients)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.Ingredient)
                .WithMany(x => x.ProductIngredients)
                .HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.NoAction);

            b.ToTable("ProductIngredients");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.AddInterceptors(new DateTimeKindInterceptor());
    }
}
