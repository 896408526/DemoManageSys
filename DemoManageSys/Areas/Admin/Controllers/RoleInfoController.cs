using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Common;
using IBLL;
using Model;
using Models.DTO;
using System.Data;
using System.Data.SqlClient;
using DemoManageSys.Areas.Filters;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models;

namespace DemoManageSys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyFilter]
    public class RoleInfoController : Controller
    {
        private IRoleInfoBLL _roleInfoBLL;
        private IUserInfoBLL _userInfoBLL;
        private IMenuInfoBLL _menuInfoBLL;

        public RoleInfoController(
            IRoleInfoBLL roleInfoBLL,
            IMenuInfoBLL menuInfoBLL,
            IUserInfoBLL userInfoBLL
            )
        {
            _roleInfoBLL = roleInfoBLL;
            _menuInfoBLL = menuInfoBLL;
            _userInfoBLL = userInfoBLL;
        }

        #region 显示视图
        public IActionResult ListView()
        {
            return View();
        }

        public IActionResult CreateView()
        {
            return View();
        }
        public IActionResult EditView()
        {
            return View();
        }
        
        public IActionResult BindUserInfoView()
        {
            return View();
        }

        public IActionResult BindMenuInfoView()
        {
            return View();
        }
        #endregion

        #region 获取角色表的方法 (GetRoleInfoes)
        /// <summary>
        /// 获取角色表的方法
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="roleName"></param>
        /// <ReturnResules></ReturnResules>
        [HttpGet]
        public IActionResult GetRoleInfoes(int limit, int page, string roleName)
        {
            int count;
            List<GetRoleInfoDTO> getRoleInfoDTOs = _roleInfoBLL.GetRoleInfos(limit, page, roleName, out count);
            ReturnResule resule = new ReturnResule()
            {
                Code = 0,
                Msg = "获取成功",
                Data = getRoleInfoDTOs,
                Count = count
            };
            return Json(resule);
        }
        #endregion

        #region 添加角色的方法 (CreateRoleInfo)
        /// <summary>
        /// 添加角色的方法
        /// </summary>
        /// <param name="role"></param>
        /// <ReturnResules></ReturnResules>
        [HttpPost]
        public IActionResult CreateRoleInfos([FromBody] RoleInfo role)
        {
            ReturnResule resule = new ReturnResule();
            string msg;
            resule.IsSuccess = _roleInfoBLL.CreateRoleInfo(role, out msg);
            if (resule.IsSuccess)
            {
                resule.Code = 200;
                resule.Msg = msg;
            }
            return Json(resule);
        }
        #endregion

        #region 删除的方法 (DeleteUserInfo)
        /// <summary>
        /// 删除的方法
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="msg">返回消息</param>
        /// <ReturnResules></ReturnResules>
        [HttpPost]
        public IActionResult DeleteRoleInfo(string id)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();
            if (string.IsNullOrWhiteSpace(id))
            {
                resule.Msg = "删除用户的id不能为空";
                return Json(resule);
            }

            resule.IsSuccess = _roleInfoBLL.DeleteRoleInfo(id);//调用方法传值
            if (resule.IsSuccess)
            {
                resule.Msg = "删除用户成功";
                resule.Code = 200;
            }
            else
            {
                resule.Msg = "删除用户失败";
            }
            return Json(resule);
        }
        #endregion

        #region 批量软删除用户的方法 (DeleteListRoleInfo)
        /// <summary>
        /// 批量软删除用户的方法
        /// </summary>
        /// <param name="role">用户实体</param>
        /// <param name="msg">返回消息</param>
        /// <ReturnResules></ReturnResules>
        [HttpPost]
        public IActionResult DeleteListRoleInfo(List<string> ids)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();
            if (ids.Count == 0)
            {
                resule.Msg = "你没有返回任何用户";
                return Json(resule);
            }
            bool isOK = _roleInfoBLL.DeleteListRolerInfo(ids);//调用方法传值
            resule.Code = 200;
            resule.IsSuccess = isOK;
            resule.Msg = "删除用户成功";
            return Json(resule);
        }
        #endregion

        #region 修改用户的方法 (UpdateRoleInfo)
        /// <summary>
        ///修改用户的方法
        /// </summary>
        /// <param name="role"></param>
        /// <ReturnResules></ReturnResules>
        [HttpPost]
        public IActionResult UpdateRoleInfo([FromBody] RoleInfo role)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();

            string msg;//定义返回消息
            resule.IsSuccess = _roleInfoBLL.UpdateRoleInfo(role, out msg);//调用方法传值

            resule.Msg = msg;
            if (resule.IsSuccess)
            {
                resule.Code = 200;
            }

            return Json(resule);
        }
        #endregion      

        #region 根据ID获取下拉框 (GetDepartmentInfoById)
        /// <summary>
        /// 根据ID获取下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <ReturnResules></ReturnResules>
        [HttpGet]
        public IActionResult GetRoleInfoById(string id)
        {
            ReturnResule resule = new ReturnResule();

            if (string.IsNullOrWhiteSpace(id))
            {
                resule.Msg = "角色ID为空";
                return Json(resule);
            }

            RoleInfo role = _roleInfoBLL.GetRoleInfoById(id);
            resule.Msg = "成功";
            resule.Code = 200;
            resule.Data = new
            {
                role = role,//部门信息
            };
            return Json(resule);
        }
        #endregion

        #region 获取未绑定角色的用户数据 (GetUserInfoOptions)
        /// <summary>
        ///获取未绑定角色的用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <ReturnResules></ReturnResules>
        public IActionResult GetUserInfoOptions(string id)
        {
            ReturnResule resule = new ReturnResule();
            if (string.IsNullOrWhiteSpace(id))
            {
                resule.Msg = "角色ID不能为空";
                return Json(resule);
            }
            List<GetUserInfoDTO> options = _userInfoBLL.GetUserInfoDTOs();

            List<string> userId = _roleInfoBLL.GetBindUserList(id);

            resule.Data = new
            {
                options,
                userId
            };
            resule.Msg = "获取成功";
            resule.Code = 200;
            return Json(resule);
        }
        #endregion

        #region 为用户绑定结果 (BindUserInfo)
        /// <summary>
        /// 为用户绑定结果
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <ReturnResules></ReturnResules>
        [HttpPost]
        public IActionResult BindUserInfo(List<string> userIds, string roleId)
        {
            ReturnResule resule = new ReturnResule();
            if (string.IsNullOrWhiteSpace(roleId))
            {
                resule.Msg = "角色ID不能为空";
                return Json(resule);
            }
            
            resule.IsSuccess = _roleInfoBLL.BindUserInfo(userIds, roleId);
            if (resule.IsSuccess)
            {
                resule.Msg = "绑定成功";
                resule.Code = 200;
            }

            return Json(resule);
        }
        #endregion

        #region 获取未绑定角色的菜单数据 (GetMenuInfoOptions)
        /// <summary>
        ///获取未绑定角色的用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <ReturnResules></ReturnResules>
        public IActionResult GetMenuInfoOptions(string id)
        {
            ReturnResule resule = new ReturnResule();
            if (string.IsNullOrWhiteSpace(id))
            {
                resule.Msg = "角色ID不能为空";
                return Json(resule);
            }
            List<GetMenuInfoDTO> options = _menuInfoBLL.GetMenuInfoDTOs();

            List<string> menuId = _roleInfoBLL.GetBindMenuList(id);

            resule.Data = new
            {
                options,
                menuId
            };
            resule.Msg = "获取成功";
            resule.Code = 200;
            return Json(resule);
        }
        #endregion

        #region 为菜单绑定结果 (BindMenuInfo)
        /// <summary>
        /// 为用户绑定结果
        /// </summary>
        /// <param name="menuIds"></param>
        /// <param name="roleId"></param>
        /// <ReturnResules></ReturnResules>
        [HttpPost]
        public IActionResult BindMenuInfo(List<string> menuIds, string roleId)
        {
            ReturnResule resule = new ReturnResule();
            if (string.IsNullOrWhiteSpace(roleId))
            {
                resule.Msg = "角色ID不能为空";
                return Json(resule);
            }

            resule.IsSuccess = _roleInfoBLL.BindMenuInfo(menuIds, roleId);
            if (resule.IsSuccess)
            {
                resule.Msg = "绑定成功";
                resule.Code = 200;
            }

            return Json(resule);
        }
        #endregion
    }
}