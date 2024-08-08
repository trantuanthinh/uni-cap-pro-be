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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var active_status_converter = new EnumToStringConverter<ActiveStatus>();
			var user_type_converter = new EnumToStringConverter<UserType>();
			var delivery_status_converter = new EnumToStringConverter<DeliveryStatus>();

			// user table
			modelBuilder.Entity<User>().HasKey(entity => entity.Id);
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasIndex(e => e.Username).IsUnique();
				entity.HasIndex(e => e.Email).IsUnique();
				entity.HasIndex(e => e.PhoneNumber).IsUnique();
			});
			modelBuilder.Entity<User>().Property(e => e.Active_Status).HasConversion(active_status_converter);
			modelBuilder.Entity<User>().Property(e => e.User_Type).HasConversion(user_type_converter);

			// product table
			modelBuilder.Entity<Product>().HasKey(entity => entity.Id);
			modelBuilder.Entity<Product>().Property(e => e.Active_Status).HasConversion(active_status_converter);

			// product image table
			modelBuilder.Entity<Product_Image>().HasKey(entity => entity.Id);

			// product category table
			modelBuilder.Entity<Product_Category>().HasKey(entity => entity.Id);
			modelBuilder.Entity<Product_Category>(entity =>
			{
				entity.HasIndex(e => e.Name).IsUnique();
			});
			modelBuilder.Entity<Product_Category>().Property(e => e.Active_Status).HasConversion(active_status_converter);

		}
	}
}
