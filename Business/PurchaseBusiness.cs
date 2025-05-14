using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class PurchaseBusiness
    {
        private readonly PurchaseRepository _repository;

        public PurchaseBusiness(PurchaseRepository repository)
        {
            _repository = repository;
        }

        public List<Purchase> GetAll()
        {
            return _repository.GetAll();
        }

        public Purchase? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Purchase? Create(Purchase purchase)
        {
            return _repository.Add(purchase);
        }

        public bool Update(Purchase purchase)
        {
            return _repository.Update(purchase);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
