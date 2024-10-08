﻿using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	// DONE
	public interface IDiscountService
	{
		Task<ICollection<Discount>> GetDiscounts();
		Task<Discount> GetDiscount(Guid id);
		Task<bool> CreateDiscount(Discount item);
		Task<bool> UpdateDiscount(Discount item, Discount patchItem);
		Task<bool> DeleteDiscount(Discount item);
	}
}
