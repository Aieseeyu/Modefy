using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class AdminUserBusiness
    {
        private readonly AdminUserRepository _repository;

        public AdminUserBusiness(AdminUserRepository repository)
        {
            _repository = repository;
        }

        public List<AdminUser> GetAll()
        {
            return _repository.GetAll();
        }

        public AdminUser? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public AdminUser? Create(AdminUser user)
        {
            return _repository.Add(user);
        }

        public bool Update(AdminUser user)
        {
            return _repository.Update(user);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
