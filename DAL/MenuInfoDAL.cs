using IDAL;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MenuInfoDAL : BaseDeleteDAL<MenuInfo>, IMenuInfoDAL
    {
        private DemoManageSysDbContext _dbContext;

        public MenuInfoDAL(
            DemoManageSysDbContext dbContext
            ):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
