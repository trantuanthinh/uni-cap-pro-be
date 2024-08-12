using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Data
{
	public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<Product_Image> Product_Images { get; set; }
		public DbSet<Product_Category> Product_Categories { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<Order_Detail> Order_Details { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var active_status_converter = new EnumToStringConverter<ActiveStatus>();
			var user_type_converter = new EnumToStringConverter<UserType>();
			var delivery_status_converter = new EnumToStringConverter<DeliveryStatus>();

			// user
			modelBuilder.Entity<User>().HasKey(entity => entity.Id);
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasIndex(e => e.Username).IsUnique();
				entity.HasIndex(e => e.Email).IsUnique();
				entity.HasIndex(e => e.PhoneNumber).IsUnique();
			});
			modelBuilder
				.Entity<User>()
				.Property(e => e.Active_Status)
				.HasConversion(active_status_converter);
			modelBuilder
				.Entity<User>()
				.Property(e => e.User_Type)
				.HasConversion(user_type_converter);

			// product
			modelBuilder.Entity<Product>().HasKey(entity => entity.Id);
			modelBuilder
				.Entity<Product>()
				.Property(e => e.Active_Status)
				.HasConversion(active_status_converter);
			modelBuilder.Entity<Product>().HasOne(p => p.Owner).WithMany(u => u.Products).HasForeignKey(p => p.OwnerId);

			// product image
			modelBuilder.Entity<Product_Image>().HasKey(entity => entity.Id);
			modelBuilder.Entity<Product_Image>().HasOne(pi => pi.Product).WithMany(p => p.Images).HasForeignKey(pi => pi.ProductId);


			// product category
			modelBuilder.Entity<Product_Category>().HasKey(entity => entity.Id);
			modelBuilder.Entity<Product_Category>(entity =>
			{
				entity.HasIndex(e => e.Name).IsUnique();
			});

			// order
			modelBuilder.Entity<Order>().HasKey(entity => entity.Id);
			modelBuilder.Entity<Order>().HasOne(o => o.Owner).WithMany(u => u.Orders).HasForeignKey(o => o.OwnerId);

			// order detail
			modelBuilder.Entity<Order_Detail>().HasKey(entity => entity.Id);
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
