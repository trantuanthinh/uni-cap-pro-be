using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	// DONE
	public interface IDiscount_DetailService
	{
		Task<ICollection<Discount_Detail>> GetDiscount_Details();
		Task<Discount_Detail> GetDiscount_Detail(Guid id);
		Task<bool> CreateDiscount_Detail(Discount_Detail item);
		Task<bool> UpdateDiscount_Detail(Discount_Detail item, Discount_Detail patchItem);
		Task<bool> DeleteDiscount_Detail(Discount_Detail item);
		Task<List<Discount_Detail>> GetDetailsByDiscountId(Guid DiscountId);
	}
}
