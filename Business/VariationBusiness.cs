using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class VariationBusiness
    {
        private readonly VariationRepository _repository;

        public VariationBusiness(VariationRepository repository)
        {
            _repository = repository;
        }

        public List<Variation> GetAll()
        {
            return _repository.GetAll();
        }

        public Variation? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Variation? Create(Variation variation)
        {
            return _repository.Add(variation);
        }

        public bool Update(Variation variation)
        {
            return _repository.Update(variation);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
