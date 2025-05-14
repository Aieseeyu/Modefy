using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class ProductVariantBusiness
    {
        private readonly ProductVariantRepository _repository;

        public ProductVariantBusiness(ProductVariantRepository repository)
        {
            _repository = repository;
        }

        public List<ProductVariant> GetAll()
        {
            return _repository.GetAll();
        }

        public ProductVariant? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ProductVariant? Create(ProductVariant variant)
        {
            return _repository.Add(variant);
        }

        public bool Update(ProductVariant variant)
        {
            return _repository.Update(variant);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
