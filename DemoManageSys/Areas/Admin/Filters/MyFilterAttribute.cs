using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoManageSys.Areas.Filters
{
    public class MyFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 执行MVC方法之前触发
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //是否登录session
            var userId = filterContext.HttpContext.Session.GetString("UserId");
            //是否登录cookie
            var userIdcookie = filterContext.HttpContext.Request.Cookies["UserId"];
            //非空判断
            if (userId == null)
            {
                //重定向到登录方法
                filterContext.Result = new RedirectResult("/Account/Login_View");
            }
        }

        /// <summary>
        /// 执行MVC方法之后触发
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }



    }
}
