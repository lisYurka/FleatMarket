using FleatMarket.Infrastructure.Data;
using FleatMarket.Infrastructure.Repositories;
using FleatMarket.Base.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FleatMarket.Service.BusinessLogic;
using FleatMarket.Base.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FleatMarket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MarketDb")));
            services.AddScoped<DbContext, DataContext>();
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<RoleManager<Role>>();
            services.AddScoped<UserManager<User>>();
            services.AddIdentity<User, Role>().AddDefaultTokenProviders().AddRoles<Role>().AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication().AddGoogle(options =>
                {
                    options.ClientId = "875591813759-caesbn5n0qmu8gu2ai2ae418riaukaba.apps.googleusercontent.com";
                    options.ClientSecret = "eaI3YGrFJxBGGlo3Mehg-eM-";
                });

            services.AddMvc();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
