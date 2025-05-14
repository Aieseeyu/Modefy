using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class ChatBusiness
    {
        private readonly ChatRepository _repository;

        public ChatBusiness(ChatRepository repository)
        {
            _repository = repository;
        }

        public List<Chat> GetAll()
        {
            return _repository.GetAll();
        }

        public Chat? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Chat? Create(Chat chat)
        {
            return _repository.Add(chat);
        }

        public bool Update(Chat chat)
        {
            return _repository.Update(chat);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
