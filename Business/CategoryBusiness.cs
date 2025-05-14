using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class CategoryBusiness
    {
        private readonly CategoryRepository _repository;

        public CategoryBusiness(CategoryRepository repository)
        {
            _repository = repository;
        }

        public List<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Category? Create(Category category)
        {
            return _repository.Add(category);
        }

        public bool Update(Category category)
        {
            return _repository.Update(category);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
