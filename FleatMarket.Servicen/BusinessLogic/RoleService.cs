﻿using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Service.BusinessLogic
{
    public class RoleService:IRoleService
    {
        private readonly IBaseRepository repository;
        public RoleService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<Role> GetRoles()
        {
            return repository.GetAll<Role>();
        }
    }
}
