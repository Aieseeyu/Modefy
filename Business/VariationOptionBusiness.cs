using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class VariationOptionBusiness
    {
        private readonly VariationOptionRepository _repository;

        public VariationOptionBusiness(VariationOptionRepository repository)
        {
            _repository = repository;
        }

        public List<VariationOption> GetAll()
        {
            return _repository.GetAll();
        }

        public VariationOption? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public VariationOption? Create(VariationOption option)
        {
            return _repository.Add(option);
        }

        public bool Update(VariationOption option)
        {
            return _repository.Update(option);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
