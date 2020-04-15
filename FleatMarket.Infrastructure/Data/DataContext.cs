using FleatMarket.Base.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FleatMarket.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User, Role, string>//IdentityDbContext<User>//DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<DeclarationStatus> DeclarationStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Путешествия"
                },
                new Category
                {
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

            model.Entity<Image>().HasData(
                new Image
                {
                    Id = 1,
                    ImageName = "DefaultDeclarationImage",
                    ImagePath = "/images/default_decl_image.jpg"
                },
                new Image
                {
                    Id = 2,
                    ImageName = "DefaultUserImage",
                    ImagePath = "/images/default_user_image.jpg"
                });

            model.Entity<DeclarationStatus>().HasData(
                new DeclarationStatus
                {
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
                new Role
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    RoleName = "user"
                },
                new Role
                {
                    Id = "2",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    RoleName = "admin"
                });

            base.OnModelCreating(model);
        }
    }
}
