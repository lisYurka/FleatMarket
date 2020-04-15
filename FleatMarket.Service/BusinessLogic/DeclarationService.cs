using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System;
using System.Collections.Generic;

namespace FleatMarket.Service.BusinessLogic
{
    public class DeclarationService:IDeclarationService
    {
        private readonly IBaseRepository repository;

        public DeclarationService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<Declaration> GetAllDeclarations()
        {
            return repository.GetWithInclude<Declaration>("Category","DeclarationStatus","User","Image");
        }

        public void RemoveDeclaration(int? id)
        {
            if (id == null)
                throw new Exception("Id can't be null!");
            else
            {
                Declaration declaration = repository.GetWithIncludeById<Declaration>(id.Value, "Category", "DeclarationStatus", "User");
                repository.Remove(declaration);
            }
        }

        public Declaration GetDeclarationById(int? id)
        {
            if (id == null)
                throw new Exception("Id can't be null!");
            else
                return repository.GetWithIncludeById<Declaration>(id.Value, "Category", "DeclarationStatus", "User","Image");
        }

        public void UpdateDeclaration(Declaration d)
        {
            if(d == null)
                throw new Exception("Declaration can't be null!");
            else
                repository.Update(d);
        }

        public void AddDeclaration(Declaration d)
        {
            if (d == null)
                throw new Exception("Declaration can't be null!");
            else
                repository.Create(d);
            //Category category = repository.GetById<Category>(d.CategoryId);
            //User user = repository.GetWithIncludeByStringId<User>(d.UserId,"Role");
            //Declaration declaration = new Declaration
            //{
            //    CategoryId = d.CategoryId,
            //    DeclarationStatusId = d.DeclarationStatusId,
            //    Description = d.Description,
            //    Price = d.Price,
            //    TimeOfCreation = d.TimeOfCreation,
            //    Title = d.Title,
            //    UserId = d.UserId
            //};
        }
    }
}
