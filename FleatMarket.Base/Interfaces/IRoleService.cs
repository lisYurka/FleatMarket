﻿using FleatMarket.Base.Entities;
using System.Collections.Generic;

namespace FleatMarket.Base.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
    }
}
