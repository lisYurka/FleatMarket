using FleatMarket.Base.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleatMarket.Base.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsersWithRoles();
        User GetUserById(int? id);
        void CreateUser(User user);
        bool UpdateUser(User user);
        bool RemoveUser(string id);
        User GetUserByStringId(string id);
        User GetWithRoleByStringId(string id);
        string GetUserRole(string id);
        bool IsInRole(string userId, string roleName);
        User GetUserByEmail(string email);
        List<Notification> GetUserNotifs(string mail);
        int GetUserNonReadNotifsCount(string mail);

        Task CreateUserAsync(User user);
    }
}
