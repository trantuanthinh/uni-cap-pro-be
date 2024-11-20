using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Models.Setting_Data_Models;
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
        public DbSet<Product_Main_Category> Product_Main_Categories { get; set; }
        public DbSet<Product_Category> Product_Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sub_Order> Sub_Orders { get; set; }
        public DbSet<Item_Order> Item_Orders { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Discount_Detail> Discount_Details { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        // Setting-Data
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var active_status_converter = new EnumToStringConverter<ActiveStatus>();
            var user_type_converter = new EnumToStringConverter<UserType>();
            var delivery_status_converter = new EnumToStringConverter<DeliveryStatus>();

            // o: orignal, d: destination

            #region Setting-Data
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Id);

                // District n - 1 Province
                entity
                    .HasOne(o => o.Province)
                    .WithMany(d => d.Districts)
                    .HasForeignKey(o => o.ProvinceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Ward n - 1 District
                entity
                    .HasOne(o => o.District)
                    .WithMany(d => d.Wards)
                    .HasForeignKey(o => o.DistrictId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity.Property(e => e.Type).HasConversion(user_type_converter);
                entity.Property(e => e.Active_Status).HasConversion(active_status_converter);

                // User n - 1 Province
                entity
                    .HasOne(o => o.Province)
                    .WithMany()
                    .HasForeignKey(o => o.ProvinceId)
                    .OnDelete(DeleteBehavior.Cascade);

                // User n - 1 District
                entity
                    .HasOne(o => o.District)
                    .WithMany()
                    .HasForeignKey(o => o.DistrictId)
                    .OnDelete(DeleteBehavior.Cascade);

                // User n - 1 Ward
                entity
                    .HasOne(o => o.Ward)
                    .WithMany()
                    .HasForeignKey(o => o.WardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active_Status).HasConversion(active_status_converter);

                //Product n - 1 User
                entity
                    .HasOne(o => o.Owner)
                    .WithMany(d => d.Products)
                    .HasForeignKey(o => o.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);

                //Product n - 1 Category
                entity.HasOne(o => o.Category).WithMany().HasForeignKey(o => o.CategoryId);
            });
            #endregion

            #region Product_Image
            modelBuilder.Entity<Product_Image>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();

                // Product_Image n - 1 Product
                entity
                    .HasOne(o => o.Product)
                    .WithMany(d => d.Images)
                    .HasForeignKey(o => o.ProductId);
            });
            #endregion

            #region Category
            // Product_Main_Category
            modelBuilder.Entity<Product_Main_Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();

                // Product_Main_Category 1 - n Product_Category
                entity
                    .HasMany(o => o.Categories)
                    .WithOne()
                    .HasForeignKey(d => d.Main_CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
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

                // Order 1 - n Sub_Order
                entity
                    .HasMany(o => o.Sub_Orders)
                    .WithOne()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Sub_Order
            modelBuilder.Entity<Sub_Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Sub_Order 1 - n Item_Order
                entity
                    .HasMany(o => o.Item_Orders)
                    .WithOne()
                    .HasForeignKey(d => d.Sub_OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region Item_Order
            modelBuilder.Entity<Item_Order>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            #endregion

            #region Discount
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Active_Status).HasConversion(active_status_converter);

                // Discount n - 1 Discount_Detail
                entity
                    .HasMany(o => o.Discount_Details)
                    .WithOne()
                    .HasForeignKey(o => o.DiscountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Discount_Detail>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            #endregion

            #region Feedback
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            #endregion
        }
    }
}
