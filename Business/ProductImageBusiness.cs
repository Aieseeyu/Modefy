using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class ProductImageBusiness
    {
        private readonly ProductImageRepository _repository;

        public ProductImageBusiness(ProductImageRepository repository)
        {
            _repository = repository;
        }

        public List<ProductImage> GetAll()
        {
            return _repository.GetAll();
        }

        public ProductImage? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ProductImage? Create(ProductImage image)
        {
            return _repository.Add(image);
        }

        public bool Update(ProductImage image)
        {
            return _repository.Update(image);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
