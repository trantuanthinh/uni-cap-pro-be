using uni_cap_pro_be.Core.Base.Repository;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
    // DONE
    public class OrderRepository(DataContext context)
        : BaseRepository<Order, DataContext>(context) { }

    public class Sub_OrderRepository(DataContext context)
        : BaseRepository<Sub_Order, DataContext>(context) { }

    public class Item_OrderRepository(DataContext context)
        : BaseRepository<Item_Order, DataContext>(context) { }
}
