﻿using uni_cap_pro_be.Core.Base.Repository;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
    // DONE
    public class DiscountRepository(DataContext context)
        : BaseRepository<Discount, DataContext>(context) { }
}