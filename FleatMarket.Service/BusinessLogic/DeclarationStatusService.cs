using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System;
using System.Collections.Generic;

namespace FleatMarket.Service.BusinessLogic
{
    public class DeclarationStatusService: IDeclarationStatusService
    {
        private readonly IBaseRepository repository;

        public DeclarationStatusService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<DeclarationStatus> GetAllStats()
        {
            return repository.GetAll<DeclarationStatus>();
        }

        public DeclarationStatus GetStatusById(int? id)
        {
            if (id != null)
            {
                return repository.GetById<DeclarationStatus>(id.Value);
            }
            else throw new Exception("Id can't be null!");
        }
    }
}
