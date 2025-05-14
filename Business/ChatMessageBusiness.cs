using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class ChatMessageBusiness
    {
        private readonly ChatMessageRepository _repository;

        public ChatMessageBusiness(ChatMessageRepository repository)
        {
            _repository = repository;
        }

        public List<ChatMessage> GetAll()
        {
            return _repository.GetAll();
        }

        public ChatMessage? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ChatMessage? Create(ChatMessage message)
        {
            return _repository.Add(message);
        }

        public bool Update(ChatMessage message)
        {
            return _repository.Update(message);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
