using FleatMarket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Base.Interfaces
{
    public interface IDeclarationStatusService
    {
        IEnumerable<DeclarationStatus> GetAllStats();
        DeclarationStatus GetStatusById(int? id);
    }
}
