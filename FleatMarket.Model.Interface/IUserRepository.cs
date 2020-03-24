using FleatMarket.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Model.Interface
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int? id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void RemoveUser(int? id);
    }
}
