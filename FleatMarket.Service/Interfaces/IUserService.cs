using FleatMarket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Service.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int? id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void RemoveUser(int? id);
    }
}
