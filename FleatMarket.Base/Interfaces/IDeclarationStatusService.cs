using FleatMarket.Base.Entities;
using System.Collections.Generic;

namespace FleatMarket.Base.Interfaces
{
    public interface IDeclarationStatusService
    {
        IEnumerable<DeclarationStatus> GetAllStats();
        DeclarationStatus GetStatusById(int? id);
    }
}
