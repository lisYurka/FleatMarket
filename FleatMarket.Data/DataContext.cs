using FleatMarket.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<DeclarationStatus> DeclarationStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Путешествия"
                },
                new Category {
                    Id = 2,
                    CategoryName = "Развлечения"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Книги"
                },
                new Category
                {
                    Id = 4,
                    CategoryName = "Техника"
                },
                new Category
                {
                    Id = 5,
                    CategoryName = "В дар"
                },
                new Category
                {
                    Id = 6,
                    CategoryName = "Животные"
                });

            model.Entity<Declaration>().HasData(
                new Declaration {
                    CategoryId = 1,
                    DeclarationStatusId=1,
                    Description = "Не упустите момент попасть на российские Мальдивы",
                    Id = 1,
                    TimeOfCreation = DateTime.Now,
                    Title = "Путевка в Челябинск",
                    UserId = 1
                },
                new Declaration
                {
                    CategoryId = 5,
                    DeclarationStatusId = 1,
                    Description = "Заберите кота от меня подальше",
                    Id = 2,
                    TimeOfCreation = DateTime.Now,
                    Title = "Британец короткошерстный",
                    UserId = 2
                },
                new Declaration
                {
                    CategoryId = 3,
                    DeclarationStatusId = 2,
                    Description = "Увлекательное путешествие в мир волшебства",
                    Id = 3,
                    TimeOfCreation = DateTime.Now,
                    Title = "Книга Гарри Поттера",
                    UserId = 3
                });

            model.Entity<DeclarationStatus>().HasData(
                new DeclarationStatus { 
                    Id = 1,
                    StatusName = "Открыто"
                },
                new DeclarationStatus
                {
                    Id = 2,
                    StatusName = "Продано"
                },
                new DeclarationStatus
                {
                    Id = 3,
                    StatusName = "Удалено"
                });

            model.Entity<Role>().HasData(
                new Role { 
                    Id = 1,
                    RoleName = "User"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Admin"
                });

            model.Entity<User>().HasData(
                new User { 
                    EMail = "User1@mail.ru",
                    Id = 1,
                    Name = "Vasya",
                    Password = "User1",
                    Phone = "123456789",
                    RoleId = 1,
                    Surname = "Ivanov",
                },
                new User
                {
                    EMail = "User2@mail.ru",
                    Id = 2,
                    Name = "Petya",
                    Password = "User2",
                    Phone = "987654321",
                    RoleId = 1,
                    Surname = "Tushenka",
                },
                new User
                {
                    EMail = "Admin@mail.ru",
                    Id = 3,
                    Name = "Alesha",
                    Password = "Admin",
                    Phone = "192837465",
                    RoleId = 2,
                    Surname = "Popovich",
                });
        }

    }
}
