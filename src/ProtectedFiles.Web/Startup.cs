using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtectedFiles.Data;
using ProtectedFiles.Data.Interfaces;
using ProtectedFiles.Data.Repositories;
using ProtectedFiles.Domain;
using ProtectedFiles.Domain.Interfaces;
using ProtectedFiles.Web.Enums;
using ProtectedFiles.Web.Infrastructure.Authorization.Requirements;
using ProtectedFiles.Web.Infrastructure.Constants;
using System;

namespace ProtectedFiles.Web
{
    public class Startup
    {
        private const string LocalStorageConfigValue = "LocalStorage";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProtectedFilesDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSession(configuration => 
            {
                configuration.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<IItemsRepository, ItemsRepository>();

            // Change authentication scheme with one of yours.
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    // Change sign in path with one of yours.
                    options.LoginPath = new PathString("/auth/Login");
                    options.AccessDeniedPath = new PathString("/auth/AccessDenied");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", configure =>
                {
                    configure.Requirements.Add(new SessionAuthorizationRequirement((int)Roles.Admin));
                });
            });

            services.AddTransient(typeof(IFileManager), factory =>
            {
                var defaultFileManager = Configuration["FileManager:Default"];
                if (defaultFileManager == LocalStorageConfigValue)
                {
                    var config = new LocalStorageFileManagerOptions();
                    config.Directory = Configuration["FileManager:LocalStorage:Directory"];
                    return new LocalStorageFileManager(config);
                }

                // Add other implementations
                return null;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseStaticFiles();

            // Imitates admin sign in. Comment this out in order to check admin authorization.
            app.Use(async (context, next) =>
            {
                context.Session.SetInt32(SessionConstants.LoggedInRoleKey, (int)Roles.Admin);
                await next();
            });

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
