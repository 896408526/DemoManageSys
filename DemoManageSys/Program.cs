using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoManageSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //InitDB();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void InitDB()
        {
            var options = new DbContextOptionsBuilder<DemoManageSysDbContext>()
                .UseSqlServer("Data Source=.;Initial Catalog=DemoManageSysCore;Integrated Security=True")
                .Options;

            DemoManageSysDbContext db = new DemoManageSysDbContext(options);
            //删除
            db.Database.EnsureDeleted();
            //添加
            db.Database.EnsureCreated();
            //创建一个初始管理员
            UserInfo userInfo = new UserInfo()
            {
                Id = Guid.NewGuid().ToString(),
                Account = "Admin",
                PassWord = MD5help.Md5("123456"),
                CreateTime = DateTime.Now,
                Sex = 1,
                IsAdmin = true,
                UserName = "初始管理员"
            };

            db.UserInfo.Add(userInfo);
            db.SaveChanges();
        }
    }
}
