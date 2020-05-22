using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleatMarket.Service.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository repository;
        public UserService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public IEnumerable<User> GetAllUsersWithRoles()
        {
            return repository.GetWithInclude<User>("Role","Image");
        }

        public User GetWithRoleByStringId(string id)
        {
            return repository.GetWithIncludeByStringId<User>(id,"Role");
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

        public User GetUserByStringId(string id)
        {
            if (id != null)
            {
                User user = repository.GetWithIncludeByStringId<User>(id,"Image");
                if (user != null)
                    return user;
                else throw new Exception("Can't find user!");
            }
            else throw new Exception("Id can't be null!");
        }

        public User GetUserByEmail(string email)
        {
            if (email != null)
            {
                User user = repository.GetWithInclude<User>("Role","Image").FirstOrDefault(m => m.Email == email);
                return user;
            }
            else throw new Exception("EMail can't be null!");
        }

        public void CreateUser(User user)
        {
            repository.Create(user);
        }

        public Task CreateUserAsync(User user)
        {
            return repository.CreateAsync(user);
        }

        public bool UpdateUser(User user)
        {
            if (user != null)
            {
                var email_count = repository.GetAll<User>().Count(item => item.Email == user.Email);
                if (email_count == 1)
                {
                    repository.Update(user);
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public bool RemoveUser(string id)
        {
            if (id != null)
            {
                User user = repository.GetByStringId<User>(id);
                repository.Remove(user);
                return true;
            }
            else return false;
        }

        public string GetUserRole(string id)
        {
            if (id != null)
            {
                var user = repository.GetWithIncludeByStringId<User>(id, "Role");
                return user.Role.RoleName;
            }
            else return null;
        }

        public bool IsInRole(string userId, string roleName)
        {
            var user = repository.GetWithIncludeByStringId<User>(userId, "Role");
            if (user.Role.RoleName == roleName)
                return true;
            else return false;
        }

        public List<Notification> GetUserNotifs(string mail)
        {
            var user = GetUserByEmail(mail);
            if (user != null)
            {
                var notifs = repository.GetAll<Notification>().Where(u => u.UserId == user.Id).ToList();
                return notifs;
            }
            else return null;
        }

        public int GetUserNonReadNotifsCount(string mail)
        {
            var user = GetUserByEmail(mail);
            if (user != null)
            {
                return repository.GetAll<Notification>().Where(u => u.UserId == user.Id && u.IsRead == false).Count();
            }
            else throw new Exception("User can't be null!");
        }
    }
}
