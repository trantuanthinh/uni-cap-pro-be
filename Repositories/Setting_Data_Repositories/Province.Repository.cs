using uni_cap_pro_be.Core.Base.Repository;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Models.Setting_Data_Models;

namespace uni_cap_pro_be.Repositories.Setting_Data_Repositories
{
    // DONE
    public class ProvinceRepository(DataContext context)
        : BaseRepositoryAddress<Province, DataContext>(context) { }
}
