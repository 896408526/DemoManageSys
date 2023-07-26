using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DemoManageSysDbContext : DbContext
    {
        public DemoManageSysDbContext(DbContextOptions<DemoManageSysDbContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<DepartmentInfo> DepartmentInfo { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<MenuInfo> MenuInfo { get; set; }
        public DbSet<R_RoleInfo_MenuInfo> R_RoleInfo_MenuInfo { get; set; }
        public DbSet<R_UserInfo_RoleInfo> R_UserInfo_RoleInfo { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<ShopInfo> ShopInfo { get; set; }
        public DbSet<SupplierInfo> SupplierInfo { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
    }
}
