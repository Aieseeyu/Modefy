using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class DeliveryTypeBusiness
    {
        private readonly DeliveryTypeRepository _repository;

        public DeliveryTypeBusiness(DeliveryTypeRepository repository)
        {
            _repository = repository;
        }

        public List<DeliveryType> GetAll()
        {
            return _repository.GetAll();
        }

        public DeliveryType? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public DeliveryType? Create(DeliveryType deliveryType)
        {
            return _repository.Add(deliveryType);
        }

        public bool Update(DeliveryType deliveryType)
        {
            return _repository.Update(deliveryType);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
