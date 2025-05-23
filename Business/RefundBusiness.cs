﻿using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

namespace ModefyEcommerce.Business
{
    public class RefundBusiness
    {
        private readonly RefundRepository _repository;

        public RefundBusiness(RefundRepository repository)
        {
            _repository = repository;
        }

        public List<Refund> GetAll()
        {
            return _repository.GetAll();
        }

        public Refund? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Refund? Create(Refund refund)
        {
            return _repository.Add(refund);
        }

        public bool Update(Refund refund)
        {
            return _repository.Update(refund);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
