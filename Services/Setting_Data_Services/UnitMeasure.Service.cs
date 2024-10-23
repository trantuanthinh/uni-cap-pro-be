using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Repositories.Setting_Data_Repositories;

namespace uni_cap_pro_be.Services.Setting_Data_Services
{
    // DONE
    public class UnitMeasureService(UnitMeasureRepository repository)
    {
        private readonly UnitMeasureRepository _repository = repository;

        public async Task<ICollection<UnitMeasure>> GetUnitMeasures()
        {
            ICollection<UnitMeasure> _items = _repository.SelectAll().ToList();
            return _items;
        }

        public async Task<UnitMeasure> GetUnitMeasure(Guid id)
        {
            UnitMeasure _item = _repository.SelectById(id);
            return _item;
        }

        public async Task<bool> CreateUnitMeasure(UnitMeasure _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteUnitMeasure(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
