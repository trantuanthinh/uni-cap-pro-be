using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var activeStatusConverter = new EnumToStringConverter<ActiveStatus>();
			var userTypeConverter = new EnumToStringConverter<UserType>();
			var deliveryStatusConverter = new EnumToStringConverter<DeliveryStatus>();

			// user table
			modelBuilder.Entity<User>().HasKey(entity => entity.Id);
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasIndex(e => e.Username).IsUnique();
				entity.HasIndex(e => e.Email).IsUnique();
				entity.HasIndex(e => e.PhoneNumber).IsUnique();
			});

			// product table
			modelBuilder.Entity<Product>().HasKey(entity => entity.Id);

			// product image table
			modelBuilder.Entity<ProductImage>().HasKey(entity => entity.Id);

			// product category table
			modelBuilder.Entity<ProductCategory>().HasKey(entity => entity.Id);
			modelBuilder.Entity<ProductCategory>(entity =>
			{
				entity.HasIndex(e => e.Name).IsUnique();
			});
		}
	}
}
