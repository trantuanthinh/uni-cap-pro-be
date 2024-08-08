using uni_cap_pro_be.Data;

namespace uni_cap_pro_be
{
	public class DatabaseSeeder(DataContext dataContext)
	{
		private readonly DataContext _dataContext = dataContext;

		public void SeedDataContext()
		{
			try
			{
				// Ensure the database is created
				_dataContext.Database.EnsureCreated();

				bool isUsers = _dataContext.Users.Any();


				bool isSeeded = isUsers;
				// Check if data already exists
				if (isSeeded)
				{
					Console.WriteLine("Database already seeded.");
					return;
				}



				_dataContext.SaveChanges();
				Console.WriteLine("Database seeding completed.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				Console.WriteLine($"Stack Trace: {ex.StackTrace}");
			}
		}
	}
}
