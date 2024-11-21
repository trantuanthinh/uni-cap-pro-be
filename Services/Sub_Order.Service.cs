using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Exceptions;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Sub_OrderService(Sub_OrderRepository repository) : BaseService()
    {
        private readonly Sub_OrderRepository _repository = repository;

        public async Task<bool> CreateSub_Order(Sub_Order _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteSub_Order(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }

        public async Task<bool> UpdateSub_OrderRating(Guid id)
        {
            Sub_Order _item = _repository.SelectById(id);
            _repository.Update(_item);
            return await _repository.SaveAsync();
        }
    }
}
