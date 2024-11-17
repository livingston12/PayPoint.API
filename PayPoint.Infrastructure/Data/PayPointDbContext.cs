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
    public DbSet<SubCategoryEntity> SubCategories { get; set; }
    public DbSet<ProductIngredientEntity> ProductIngredients { get; set; }
    public DbSet<IngredientEntity> Ingredients { get; set; }
    public DbSet<TableEntity> Tables { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    public DbSet<OrderStatusEntity> OrderStatus { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PaymentEntity> Payments { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Products relationship
        modelBuilder.Entity<ProductEntity>(b =>
        {
            b.HasKey(e => e.ProductId);
            b.Property(e => e.ProductId).ValueGeneratedOnAdd();
            b.Property(e => e.Price).HasPrecision(18, 2).IsRequired();
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

        // Categories relationship
        modelBuilder.Entity<CategoryEntity>(b =>
        {
            b.HasKey(x => x.CategoryId);
            b.Property(x => x.CategoryId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.Description).HasMaxLength(150).HasDefaultValue(string.Empty);
            b.HasMany(x => x.SubCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            b.ToTable("Categories");
        });

        // SubCategories relationship
        modelBuilder.Entity<SubCategoryEntity>(b =>
        {
            b.HasKey(x => x.SubCategoryId);
            b.Property(x => x.SubCategoryId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.Description).HasMaxLength(150).HasDefaultValue(string.Empty);
            b.ToTable("SubCategories");
        });

        // Ingredients relationship
        modelBuilder.Entity<IngredientEntity>(b =>
        {
            b.HasKey(x => x.IngredientId);
            b.Property(x => x.IngredientId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.AdditionalPrice).HasPrecision(12, 2);
            b.HasMany(x => x.ProductIngredients)
                .WithOne(x => x.Ingredient)
                .HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Ingredients");
        });

        // ProductIngredients relationship
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

        // Tables relationship
        modelBuilder.Entity<TableEntity>(b =>
        {
            b.HasKey(x => x.TableId);
            b.Property(x => x.TableId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.Description).HasMaxLength(150).HasDefaultValue(string.Empty);
            b.HasMany(x => x.Orders)
                .WithOne(x => x.Table)
                .HasForeignKey(x => x.TableId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Tables");
        });

        // Rooms relationship
        modelBuilder.Entity<RoomEntity>(b =>
        {
            b.HasKey(x => x.RoomId);
            b.Property(x => x.RoomId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.Description).HasMaxLength(150).HasDefaultValue(string.Empty);
            b.HasMany(x => x.Tables)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Rooms");
        });

        // Orders relationship
        modelBuilder.Entity<OrderEntity>(b =>
        {
            b.HasKey(x => x.OrderId);
            b.Property(x => x.OrderId).ValueGeneratedOnAdd();
            b.Property(x => x.TableId).IsRequired();
            b.Property(x => x.UserId).IsRequired();
            b.Property(x => x.OrderStatusId).IsRequired();
            b.HasOne(x => x.Table)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.TableId)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.OrderStatusId)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasMany(x => x.Payments)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Orders");
        });

        // Order details relationship.
        modelBuilder.Entity<OrderDetailEntity>(b =>
        {
            b.HasKey(x => new { x.OrderId, x.ProductId });
            b.Property(x => x.Quantity).IsRequired();
            b.Property(x => x.UnitPrice).HasPrecision(18, 2).IsRequired();
            b.Property(x => x.Discount).HasPrecision(18, 2);
            b.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("OrderDetails");
        });

        // Payments relationship
        modelBuilder.Entity<PaymentEntity>(b =>
        {
            b.HasKey(x => x.PaymentId);
            b.Property(x => x.PaymentId).ValueGeneratedOnAdd();
            b.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
            b.HasOne(x => x.Order)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Payments");
        });

        // Invoice relationship
        modelBuilder.Entity<InvoiceEntity>(b =>
        {
            b.HasKey(x => x.InvoiceId);
            b.Property(x => x.InvoiceId).ValueGeneratedOnAdd();
            b.Property(x => x.Detail).HasMaxLength(100);
            b.Property(x => x.TotalAmount).HasPrecision(18, 2);
            b.HasOne(x => x.Customer)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            b.HasOne(x => x.User)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            b.HasOne(x => x.Order)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            b.ToTable("Invoices");
        });

        // OrderStatuses relationship
        modelBuilder.Entity<OrderStatusEntity>(b =>
        {
            b.HasKey(x => x.OrderStatusId);
            b.Property(x => x.OrderStatusId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.Description).HasMaxLength(150).HasDefaultValue(string.Empty);
            b.ToTable("OrderStatus");
        });

        // User relationship
        modelBuilder.Entity<UserEntity>(b =>
        {
            b.HasKey(x => x.UserId);
            b.Property(x => x.UserId).ValueGeneratedOnAdd();
            b.Property(x => x.FirstName).HasMaxLength(75).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(75).IsRequired();
            b.Property(x => x.Email).HasMaxLength(75).IsRequired();
            b.Property(x => x.IsActive).HasDefaultValue(true);
            b.ToTable("Users");
        });

        // Customer relationship
        modelBuilder.Entity<CustomerEntity>(b =>
        {
            b.HasKey(x => x.CustomerId);
            b.Property(x => x.CustomerId).ValueGeneratedOnAdd();
            b.Property(x => x.FirstName).HasMaxLength(75).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(75).IsRequired();
            b.Property(x => x.Phone).HasMaxLength(15);
            b.Property(x => x.Email).HasMaxLength(75);
            b.ToTable("Customers");
        });

        // PaymentMethods relationship
        modelBuilder.Entity<PaymentMethodEntity>(b =>
        {
            b.HasKey(x => x.PaymentMethodId);
            b.Property(x => x.PaymentMethodId).ValueGeneratedOnAdd();
            b.Property(x => x.Name).HasMaxLength(75).IsRequired();
            b.Property(x => x.Description).HasMaxLength(150).HasDefaultValue(string.Empty);
            b.HasMany(x => x.Payments)
                .WithOne(x => x.PaymentMethod)
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("PaymentMethods");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.AddInterceptors(new DateTimeKindInterceptor());
    }
}
