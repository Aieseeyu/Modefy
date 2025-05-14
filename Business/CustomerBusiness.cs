using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class CustomerBusiness
    {
        private readonly CustomerRepository _repository;

        public CustomerBusiness(CustomerRepository repository)
        {
            _repository = repository;
        }

        public List<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public Customer? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Customer? Create(Customer customer)
        {
            return _repository.Add(customer);
        }

        public bool Update(Customer customer)
        {
            return _repository.Update(customer);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
