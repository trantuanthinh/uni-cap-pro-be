using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(entity => entity.Id);
			modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Username).IsUnique(); });
			modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
			modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.PhoneNumber).IsUnique(); });

			base.OnModelCreating(modelBuilder);
		}
	}
}
