using FleatMarket.Base.Entities;
using System.Collections.Generic;

namespace FleatMarket.Base.Interfaces
{
    public interface IDeclarationService
    {
        IEnumerable<Declaration> GetAllDeclarations();
        Declaration GetDeclarationById(int? id);
        void RemoveDeclaration(int? id);
        void UpdateDeclaration(Declaration d);
        void AddDeclaration(Declaration d);
    }
}
