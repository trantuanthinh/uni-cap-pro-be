using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Product_Image> Product_Images { get; set; }
    public DbSet<Product_Category> Product_Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Sub_Order> Sub_Orders { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Discount_Detail> Discount_Details { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var active_status_converter = new EnumToStringConverter<ActiveStatus>();
        var user_type_converter = new EnumToStringConverter<UserType>();
        var delivery_status_converter = new EnumToStringConverter<DeliveryStatus>();

        // user
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            entity.Property(e => e.Active_Status).HasConversion(active_status_converter);
            entity.Property(e => e.User_Type).HasConversion(user_type_converter);

            // user 1 - n product
            entity
                .HasMany(origin => origin.Products)
                .WithOne(dest => dest.Owner)
                .HasForeignKey(dest => dest.OwnerId);

            // user 1 - n sub_order
            // entity
            //     .HasMany(origin => origin.Sub_Orders)
            //     .WithOne(dest => dest.User)
            //     .HasForeignKey(dest => dest.UserId);
        });

        // product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Active_Status).HasConversion(active_status_converter);

            //product n - 1 user
            entity
                .HasOne(origin => origin.Owner)
                .WithMany(dest => dest.Products)
                .HasForeignKey(origin => origin.OwnerId);

            //product n - 1 category
            entity
                .HasOne(origin => origin.Category)
                .WithMany()
                .HasForeignKey(origin => origin.CategoryId);

            //product n - 1 discount
            entity
                .HasOne(origin => origin.Discount)
                .WithMany()
                .HasForeignKey(origin => origin.DiscountId);

            //product 1 - n image
            entity
                .HasMany(origin => origin.Images)
                .WithOne(dest => dest.Product)
                .HasForeignKey(origin => origin.ProductId);
        });

        // product image
        modelBuilder.Entity<Product_Image>(entity =>
        {
            entity.HasKey(e => e.Id);

            // product_image n - 1 product
            entity
                .HasOne(origin => origin.Product)
                .WithMany(dest => dest.Images)
                .HasForeignKey(origin => origin.ProductId);
        });

        // product category
        modelBuilder.Entity<Product_Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity
                .HasOne(origin => origin.Product)
                .WithMany()
                .HasForeignKey(origin => origin.ProductId);

            entity
                .HasMany(origin => origin.Sub_Orders)
                .WithOne()
                .HasForeignKey(origin => origin.OrderId);
        });

        // sub order
        modelBuilder.Entity<Sub_Order>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        // discount
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity
                .HasMany(origin => origin.Discount_Details)
                .WithOne()
                .HasForeignKey(origin => origin.DiscountId);
        });

        // discount detail
        modelBuilder.Entity<Discount_Detail>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}
