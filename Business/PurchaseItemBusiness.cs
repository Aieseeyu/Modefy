using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class PurchaseItemBusiness
    {
        private readonly PurchaseItemRepository _repository;

        public PurchaseItemBusiness(PurchaseItemRepository repository)
        {
            _repository = repository;
        }

        public List<PurchaseItem> GetAll()
        {
            return _repository.GetAll();
        }

        public PurchaseItem? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public PurchaseItem? Create(PurchaseItem item)
        {
            return _repository.Add(item);
        }

        public bool Update(PurchaseItem item)
        {
            return _repository.Update(item);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
