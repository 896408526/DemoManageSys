using IDAL;
using Microsoft.EntityFrameworkCore;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserInfoDAL : BaseDeleteDAL<UserInfo>,IUserInfoDAL
    {
        //实例化数据库上下文
        private DemoManageSysDbContext _dbContext;
        public UserInfoDAL (
            DemoManageSysDbContext dbContext
            ) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
