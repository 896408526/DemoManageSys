using Microsoft.EntityFrameworkCore;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    /// <summary>
    /// 用户表数据访问层接口
    /// </summary>
    public interface IUserInfoDAL : IBaseDeleteDAL<UserInfo>
    {

    }
}
