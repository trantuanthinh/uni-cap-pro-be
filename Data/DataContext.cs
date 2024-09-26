using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Data
{
    // TODO
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

            #region User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity.Property(e => e.Active_Status).HasConversion(active_status_converter);
                entity.Property(e => e.User_Type).HasConversion(user_type_converter);

                // User 1 - n Product
                entity
                    .HasMany(origin => origin.Products)
                    .WithOne(d => d.Owner)
                    .HasForeignKey(d => d.OwnerId);

                // User 1 - n sub_order
                // entity
                //     .HasMany(origin => origin.Sub_Orders)
                //     .WithOne(d => d.User)
                //     .HasForeignKey(d => d.UserId);
            });
            #endregion

            #region Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active_Status).HasConversion(active_status_converter);

                //Product n - 1 User
                entity
                    .HasOne(origin => origin.Owner)
                    .WithMany(d => d.Products)
                    .HasForeignKey(origin => origin.OwnerId);

                //Product n - 1 Category
                entity
                    .HasOne(origin => origin.Category)
                    .WithMany()
                    .HasForeignKey(origin => origin.CategoryId);

                //Product n - 1 Discount
                entity
                    .HasOne(origin => origin.Discount)
                    .WithMany()
                    .HasForeignKey(origin => origin.DiscountId);

                //Product 1 - n image
                // entity
                //     .HasMany(origin => origin.Images)
                //     .WithOne(d => d.Product)
                //     .HasForeignKey(origin => origin.ProductId);
            });
            #endregion

            #region Product_Image
            modelBuilder.Entity<Product_Image>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Product_Image n - 1 Product
                entity
                    .HasOne(origin => origin.Product)
                    .WithMany(d => d.Images)
                    .HasForeignKey(origin => origin.ProductId);
            });

            // Product_Category
            modelBuilder.Entity<Product_Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
            });
            #endregion

            #region Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Order n - 1 Product
                entity
                    .HasOne(origin => origin.Product)
                    .WithMany()
                    .HasForeignKey(origin => origin.ProductId);

                // Order 1 - n Sub_Orders
                entity
                    .HasMany(origin => origin.Sub_Orders)
                    .WithOne()
                    .HasForeignKey(origin => origin.OrderId);
            });
            #endregion

            #region Sub_Order
            modelBuilder.Entity<Sub_Order>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            #endregion

            #region Discount
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Discount 1 - n Discount_Details
                entity
                    .HasMany(origin => origin.Discount_Details)
                    .WithOne()
                    .HasForeignKey(origin => origin.DiscountId);
            });
            #endregion

            #region Discount_Detail
            modelBuilder.Entity<Discount_Detail>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            #endregion
        }
    }
}
