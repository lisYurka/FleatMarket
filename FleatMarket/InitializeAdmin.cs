using FleatMarket.Base.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FleatMarket.Web
{
    public class InitializeAdmin
    {
        public static async Task InitializeDbWithAdmin(UserManager<User> user)
        {
            string mail = "admin@mail.ru";
            string password = "BestAdmin2020";
            string name = "Vasya";
            string surname = "Pupkin";

            var find_user = await user.FindByEmailAsync(mail);
            if(find_user == null)
            {
                User admin = new User
                {
                    Email = mail,
                    UserName = mail,
                    Name = name,
                    Surname = surname,
                    RoleId = "2",
                    LastEditDate = DateTime.Now.ToString(),
                    RegistrationDate = DateTime.Now.ToString(),
                    ImageId = 2
                };
                var result = await user.CreateAsync(admin, password);
                if (result.Succeeded)
                    await user.AddClaimAsync(admin, new Claim(ClaimTypes.Role.ToString(), "Admin"));
            }
        }
    }
}
