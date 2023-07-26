﻿using IDAL;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class R_UserInfo_RoleInfoDAL : BaseDAL<R_UserInfo_RoleInfo>,IR_UserInfo_RoleInfoDAL
    {
        private DemoManageSysDbContext _dbContext;
        public R_UserInfo_RoleInfoDAL(
            DemoManageSysDbContext dbContext
            ) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
