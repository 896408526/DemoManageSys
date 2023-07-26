using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Common;
using IBLL;
using Models;
using Models.DTO;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using DemoManageSys.Areas.Filters;

namespace DemoManageSys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyFilter]
    public class UserInfoController : Controller
    {
        private IUserInfoBLL _userInfoBLL;

        public UserInfoController(
            IUserInfoBLL userInfoBLL
            )
        {
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

        public IActionResult UpdatePassWord()
        {
            return View();
        }

        public IActionResult SettingUser()
        {
            return View();
        }
        #endregion

        #region 获取的方法 (GetUserInfoes)
        /// <summary>
        /// 获取用户列表的方法
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页几行</param>
        /// <param name="userName">姓名</param>
        /// <param name="account">账号</param>
        /// <param name="count">共多少条数据</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserInfo(int page, int limit, string userName, string account, int count)
        {
            //调用BLL的查询方法
            List<GetUserInfoDTO> GetUserInfoDTOs = _userInfoBLL.GetUserInfoes(page, limit, userName, account, out count);

            ReturnResule result = new ReturnResule()
            {
                Code = 0,
                Msg = "获取成功",
                Data = GetUserInfoDTOs,
                Count = count
            };

            return Json(result);
        }
        #endregion

        #region 添加的方法 (CreateUserInfo)
        [HttpPost]
        public IActionResult CreateUserInfo([FromForm]UserInfo param)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();
            string msg;//定义返回消息
                       //调用方法添加用户
            resule.IsSuccess = _userInfoBLL.CreateUserInfo(param, out msg);//调用方法传值
            if (resule.IsSuccess)
            {
                resule.Code = 200;
                resule.Msg = msg;
            }
            else
            {
                resule.Msg = msg;
            }
            return Json(resule);
        }
        #endregion

        #region 删除的方法 (DeleteUserInfo)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteUserInfo(string id)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();
            if (string.IsNullOrWhiteSpace(id))
            {
                resule.Msg = "删除用户的id不能为空";
                return Json(resule);
            }

            resule.IsSuccess = _userInfoBLL.DeleteUserInfo(id);//调用方法传值
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

        #region 批量软删除用户的方法 (DeleteListUserInfo)
        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteListUserInfo(List<string> ids)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();
            if (ids.Count == 0)
            {
                resule.Msg = "你没有返回任何用户";
                return Json(resule);
            }
            bool isOK = _userInfoBLL.DeleteListUserInfo(ids);//调用方法传值
            resule.Code = 200;
            resule.IsSuccess = isOK;
            resule.Msg = "删除用户成功";
            return Json(resule);
        }
        #endregion

        #region 修改用户的方法 (UpdateUserInfo)
        /// <summary>
        /// 修改用户的方法
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateUserInfo([FromBody] UserInfo user)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();

            string msg;//定义返回消息
            resule.IsSuccess = _userInfoBLL.UpdateUserInfo(user, out msg);//调用方法传值

            resule.Msg = msg;
            if (resule.IsSuccess)
            {
                resule.Code = 200;
            }

            return Json(resule);
        }
        #endregion

        #region 修改用户密码的方法 (UpdatePassWordes)
        /// <summary>
        ///修改用户密码的方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <param name="agPwd"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdatePassWordes(string id, string oldPwd, string newPwd, string agPwd)
        {
            ReturnResule resule = new ReturnResule();

            string msg;
            resule.IsSuccess = _userInfoBLL.UpdatePassWord(id, oldPwd, newPwd, agPwd, out msg);
            resule.Msg = msg;
            if (resule.IsSuccess)
            {
                resule.Code = 200;
            }
            return Json(resule);

        }
        #endregion
       
    }
}
