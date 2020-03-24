using FleatMarket.Base;
using FleatMarket.Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FleatMarket.Data
{
    public class UserRepository:IUserRepository
    {
        private readonly BaseRepository repository;

        public UserRepository(BaseRepository _repository)
        {
            repository = _repository;
        }

        public List<User> GetAllUsers()
        {
            IEnumerable<User> users = repository.GetWithInclude<User>("Role");
            //List<User> userList = new List<User>();
            //foreach(var item in users)
            //{
            //    Role role = new Role
            //    {
            //        Id = item.Role.Id,
            //        RoleName = item.Role.RoleName
            //    };
            //    User user = new User
            //    {
            //        EMail = item.EMail,
            //        Id = item.Id,
            //        Name = item.Name,
            //        Password = item.Password,
            //        Phone = item.Phone,
            //        Role = role,
            //        Surname = item.Surname
            //    };
            //    userList.Add(user);
            //}
            return users.ToList();
        }
        public User GetUserById(int? id)
        {
            if (id != null)
            {
                User user = repository.GetWithIncludeById<User>(id.Value, "Role");
                if (user != null)
                    return user;
                else throw new Exception("User can't be null!");
            }
            else throw new Exception("Id can't be null!");
        }
        public void CreateUser(User user)
        {
            User newUser = new User
            {
                EMail = user.EMail,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                Surname = user.Surname,
                IsActive = true
            };
            repository.Create(newUser);
        }
        public void UpdateUser(User user)
        {
            if (user != null)
            {
                User updatedUser = repository.GetWithIncludeById<User>(user.Id, "Role");

                updatedUser.Id = user.Id;
                updatedUser.IsActive = user.IsActive;
                updatedUser.Name = user.Name;
                updatedUser.Phone = user.Phone;
                updatedUser.RoleId = user.RoleId;
                updatedUser.Surname = user.Surname;

                repository.Update(updatedUser);
            }
            else throw new Exception("User can't be null!");
        }
        public void RemoveUser(int? id)
        {
            if (id != null) {
                User user = repository.GetById<User>(id.Value);
                repository.Remove(user);
            }
            else throw new Exception("Id can't be null!");
        }
    }
}
