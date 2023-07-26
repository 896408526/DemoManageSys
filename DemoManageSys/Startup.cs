using BLL;
using DAL;
using IBLL;
using IDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoManageSys
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            #region ¿çÓò²ßÂÔ
            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            });
            #endregion

            #region IOC×¢²á
            //×¢²áUserInfoµÄDAL£¬BLL
            services.AddScoped<IUserInfoDAL, UserInfoDAL>();
            services.AddScoped<IUserInfoBLL, UserInfoBLL>();

            //×¢²áRoleInfoµÄDAL£¬BLL
            services.AddScoped<IRoleInfoDAL, RoleInfoDAL>();
            services.AddScoped<IRoleInfoBLL, RoleInfoBLL>();

            //×¢²áMenuInfoµÄDAL£¬BLL
            services.AddScoped<IMenuInfoDAL, MenuInfoDAL>();
            services.AddScoped<IMenuInfoBLL, MenuInfoBLL>();

            //×¢²áDepartmentInfoµÄDAL£¬BLL
            services.AddScoped<IDepartmentInfoBLL, DepartmentInfoBLL>();
            services.AddScoped<IDepartmentInfoDAL, DepartmentInfoDAL>();

            //×¢²áR_UserInfo_RoleInfoµÄDAL
            services.AddScoped<IR_UserInfo_RoleInfoDAL, R_UserInfo_RoleInfoDAL>();

            //×¢²áR_RoleInfo_MenuInfoµÄDAL
            services.AddScoped<IR_RoleInfo_MenuInfoDAL, R_RoleInfo_MenuInfoDAL>();
            #endregion

            services.AddDbContext<DemoManageSysDbContext>(options => options.UseSqlServer("name=ConnectionStrings:SqlServerStr"));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //Æô¶¯¿çÓò²ßÂÔ
            app.UseCors("MyCorsPolicy");

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LoginView}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Account}/{action=LoginView}/{id?}");

                endpoints.MapControllerRoute(
                    name: "APP",
                    pattern: "{area:exists}/{controller=Account}/{action=LoginView}/{id?}");
            });

        }
    }
}
