using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class PromotionBusiness
    {
        private readonly PromotionRepository _repository;

        public PromotionBusiness(PromotionRepository repository)
        {
            _repository = repository;
        }

        public List<Promotion> GetAll()
        {
            return _repository.GetAll();
        }

        public Promotion? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Promotion? Create(Promotion promotion)
        {
            return _repository.Add(promotion);
        }

        public bool Update(Promotion promotion)
        {
            return _repository.Update(promotion);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
