using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class PasswordResetBusiness
    {
        private readonly PasswordResetRepository _repository;

        public PasswordResetBusiness(PasswordResetRepository repository)
        {
            _repository = repository;
        }

        public List<PasswordReset> GetAll()
        {
            return _repository.GetAll();
        }

        public PasswordReset? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public PasswordReset? Create(PasswordReset reset)
        {
            return _repository.Add(reset);
        }

        public bool Update(PasswordReset reset)
        {
            return _repository.Update(reset);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
