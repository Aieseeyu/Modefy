using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class ProductBusiness
    {
        private readonly ProductRepository _repository;

        public ProductBusiness(ProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public Product? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Product? Create(Product product)
        {
            return _repository.Add(product);
        }

        public bool Update(Product product)
        {
            return _repository.Update(product);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
