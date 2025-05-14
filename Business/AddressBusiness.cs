using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class AddressBusiness
    {
        private readonly AddressRepository _repository;

        public AddressBusiness(AddressRepository repository)
        {
            _repository = repository;
        }

        public List<Address> GetAll()
        {
            return _repository.GetAll();
        }

        public Address? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Address? Create(Address address)
        {
            return _repository.Add(address);
        }

        public bool Update(Address address)
        {
            return _repository.Update(address);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
