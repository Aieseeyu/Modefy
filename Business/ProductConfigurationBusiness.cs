using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class ProductConfigurationBusiness
    {
        private readonly ProductConfigurationRepository _repository;

        public ProductConfigurationBusiness(ProductConfigurationRepository repository)
        {
            _repository = repository;
        }

        public List<ProductConfiguration> GetAll()
        {
            return _repository.GetAll();
        }

        public ProductConfiguration? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ProductConfiguration? Create(ProductConfiguration config)
        {
            return _repository.Add(config);
        }

        public bool Update(ProductConfiguration config)
        {
            return _repository.Update(config);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
