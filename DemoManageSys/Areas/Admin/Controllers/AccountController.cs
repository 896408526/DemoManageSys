using BLL;
using Common;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoManageSys.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        // GET: Login
        private IUserInfoBLL _userinfoBLL;

        public AccountController(
            IUserInfoBLL userinfoBLL
            )
        {
            _userinfoBLL = userinfoBLL;
        }

        public IActionResult LoginView()
        
        {
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LoginIndex(string account, string userPwd)
        {
            //序列化和反序列化
            //var jsonStr = JsonConvert.SerializeObject(result);
            //result = JsonConvert.DeserializeObject<Return>(jsonStr);
            ReturnResule result = new ReturnResule();

            if (string.IsNullOrWhiteSpace(account))
            {
                result.Msg = "账号不能为空";
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(userPwd))
            {
                result.Msg = "密码不能为空";
                return Json(result);
            }

            string msg;
            string userName;
            string userId;
            result.IsSuccess = _userinfoBLL.LoginIndex(account, userPwd, out msg, out userName, out userId);
            if (result.IsSuccess)
            {
                result.Code = 200;
                result.Data = userName;
                result.Msg = msg;
                //session
                HttpContext.Session.SetString("UserId", userId);
                HttpContext.Session.SetString("UserName", userName);
                //cookie
                CookieOptions options = new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(2)
                };
                Response.Cookies.Append("UserId", userId);
                Response.Cookies.Append("UserName", userName);

            }
            else
            {
                result.Msg = msg;
                result.Code = 500;
            }
            return Json(result);

        }
    }
}