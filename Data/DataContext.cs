using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Product_Image> Product_Images { get; set; }
		public DbSet<Product_Category> Product_Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Sub_Order> Sub_Orders { get; set; }
		public DbSet<Order_Detail> Order_Details { get; set; }
		public DbSet<Discount> Discounts { get; set; }

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

				entity.HasMany(origin => origin.Products)
					  .WithOne(dest => dest.Owner)
					  .HasForeignKey(dest => dest.OwnerId);

				entity.HasMany(origin => origin.Sub_Orders)
					  .WithOne(dest => dest.User)
					  .HasForeignKey(dest => dest.UserId);
			});

			// product
			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Active_Status).HasConversion(active_status_converter);

				entity.HasOne(origin => origin.Owner)
					  .WithMany(dest => dest.Products)
					  .HasForeignKey(origin => origin.OwnerId);

				entity.HasOne(origin => origin.Category)
					  .WithMany()
					  .HasForeignKey(origin => origin.CategoryId);

				entity.HasMany(origin => origin.Orders)
					  .WithOne(dest => dest.Product)
					  .HasForeignKey(origin => origin.ProductId);

				entity.HasMany(origin => origin.Discounts)
					  .WithOne()
					  .HasForeignKey(origin => origin.ProductId);
			});

			// product image
			modelBuilder.Entity<Product_Image>(entity =>
			{
				entity.HasKey(e => e.Id);
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
				entity.HasOne(origin => origin.Product)
					  .WithMany(dest => dest.Orders)
					  .HasForeignKey(origin => origin.ProductId);
			});

			// sub order
			modelBuilder.Entity<Sub_Order>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.HasOne(origin => origin.User)
					  .WithMany(dest => dest.Sub_Orders)
					  .HasForeignKey(origin => origin.UserId);

				entity.HasOne(origin => origin.Order)
					  .WithMany(dest => dest.Sub_Orders)
					  .HasForeignKey(origin => origin.OrderId);
			});

			// discount
			modelBuilder.Entity<Discount>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.HasOne(origin => origin.Product)
					  .WithMany(dest => dest.Discounts)
					  .HasForeignKey(origin => origin.ProductId);
			});
		}

		public void CreateProductView()
		{
			var query =
				@"
                CREATE VIEW Product_View AS
                SELECT 
                    p.Id AS Id, 
                    p.Description AS Description, 
                    p.Price AS Price, 
                    p.Total_Rating_Value AS Total_Rating_Value, 
                    p.Total_Rating_Quantity AS Total_Rating_Quantity, 
                    ps.Name AS Category, 
                    pi.Name AS Image, 
                    u.Name AS Owner
                FROM 
                    Products AS p
                INNER JOIN 
                    Product_Categories AS ps ON p.CategoryId = ps.Id
                INNER JOIN 
                    Product_Images AS pi ON p.Id = pi.ProductId
                INNER JOIN 
                    Users AS u ON p.OwnerId = u.Id;
            ";
			Database.ExecuteSqlRaw(query);
		}
	}
}
