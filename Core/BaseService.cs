namespace uni_cap_pro_be.Core
{
	// DONE
	public class BaseService
	{
		protected readonly ILogger<object> Logger;
		//protected internal IUnitOfWork UnitOfWork { get; set; }

		public BaseService(ILogger<object> logger)
		{
			Logger = logger;
		}

		public BaseService() { }

		//public BaseService(IUnitOfWork unitOfWork)
		//{
		//    UnitOfWork = unitOfWork;
		//}
	}
}
