using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Repositories.Setting_Data_Repositories;

namespace uni_cap_pro_be.Services.Setting_Data_Services
{
    // DONE
    public class AddressService(
        ProvinceRepository provinceRepository,
        DistrictRepository districtRepository,
        WardRepository wardRepository
    )
    {
        private readonly ProvinceRepository _provinceRepository = provinceRepository;
        private readonly DistrictRepository _districtRepository = districtRepository;
        private readonly WardRepository _wardRepository = wardRepository;

        public async Task<ICollection<ProvinceResponse>> GetProvinces()
        {
            ICollection<ProvinceResponse> _items = _provinceRepository
                .SelectAll()
                .Select(item => item.ToResponse())
                .ToList();
            return _items;
        }

        // Get District By Province Id
        public async Task<ICollection<DistrictResponse>> GetDistrictsByPId(string id)
        {
            ICollection<DistrictResponse> _items = _districtRepository
                .SelectAll()
                .Where(item => item.ProvinceId == id)
                .Select(item => item.ToResponse())
                .ToList();
            return _items;
        }

        // Get Wards By District Id
        public async Task<ICollection<WardResponse>> GetWardsByDId(string id)
        {
            ICollection<WardResponse> _items = _wardRepository
                .SelectAll()
                .Where(item => item.DistrictId == id)
                .Select(item => item.ToResponse())
                .ToList();
            return _items;
        }

        // Side Functions, Not Used Yet
        public async Task<Province> GetProvince(string id)
        {
            Province _item = _provinceRepository.SelectById(id);
            return _item;
        }

        public async Task<ICollection<District>> GetDistricts()
        {
            ICollection<District> _items = _districtRepository.SelectAll().ToList();
            return _items;
        }

        public async Task<District> GetDistrict(string id)
        {
            District _item = _districtRepository.SelectById(id);
            return _item;
        }

        public async Task<ICollection<Ward>> GetWards()
        {
            ICollection<Ward> _items = _wardRepository.SelectAll().ToList();
            return _items;
        }

        public async Task<Ward> GetWard(string id)
        {
            Ward _item = _wardRepository.SelectById(id);
            return _item;
        }
    }
}
