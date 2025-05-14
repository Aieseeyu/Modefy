using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class CarrierBusiness
    {
        private readonly CarrierRepository _repository;

        public CarrierBusiness(CarrierRepository repository)
        {
            _repository = repository;
        }

        public List<Carrier> GetAll()
        {
            return _repository.GetAll();
        }

        public Carrier? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Carrier? Create(Carrier carrier)
        {
            return _repository.Add(carrier);
        }

        public bool Update(Carrier carrier)
        {
            return _repository.Update(carrier);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
