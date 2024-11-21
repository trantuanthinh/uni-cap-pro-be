using uni_cap_pro_be.Core.Base.Repository;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
    // DONE
    public class ProductRepository(DataContext context)
        : BaseRepository<Product, DataContext>(context) { }

    public class Product_Main_CategoryRepository(DataContext context)
        : BaseRepository<Product_Main_Category, DataContext>(context) { }

    public class Product_CategoryRepository(DataContext context)
        : BaseRepository<Product_Category, DataContext>(context) { }

    public class Product_ImageRepository(DataContext context)
        : BaseRepository<Product_Image, DataContext>(context) { }
}
