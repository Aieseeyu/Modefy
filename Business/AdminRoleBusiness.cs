using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class AdminRoleBusiness
    {
        private readonly AdminRoleRepository _repository;

        public AdminRoleBusiness(AdminRoleRepository repository)
        {
            _repository = repository;
        }

        public List<AdminRole> GetAll()
        {
            return _repository.GetAll();
        }

        public AdminRole? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public AdminRole? Create(AdminRole role)
        {
            return _repository.Add(role);
        }

        public bool Update(AdminRole role)
        {
            return _repository.Update(role);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
