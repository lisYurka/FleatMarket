using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using FleatMarket.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Service.BusinessLogic
{
    public class UserService:IUserService
    {
        private readonly IBaseRepository repository;
        public UserService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return repository.GetAll<User>();
        }

        public User GetUserById(int? id)
        {
            if (id != null)
            {
                User user = repository.GetById<User>(id.Value);
                if (user != null)
                    return user;
                else throw new Exception("User can't be null!");
            }
            else throw new Exception("Id can't be null!");
        }

        public void CreateUser(User user)
        {
            repository.Create(user);
        }

        public void UpdateUser(User user)
        {
            if (user != null)
            {
                //User updatedUser = repository.GetById<User>(user.Id);

                //updatedUser.IsActive = user.IsActive;
                //updatedUser.Name = user.Name;
                //updatedUser.Phone = user.Phone;
                //updatedUser.RoleId = user.RoleId;
                //updatedUser.Surname = user.Surname;

                //repository.Update(updatedUser);
                repository.Update(user);
            }
            else throw new Exception("User can't be null!");
        }

        public void RemoveUser(int? id)
        {
            if (id != null)
            {
                User user = repository.GetById<User>(id.Value);
                repository.Remove(user);
            }
            else throw new Exception("Id can't be null!");
        }
    }
}
