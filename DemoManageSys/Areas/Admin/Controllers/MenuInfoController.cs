using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using DemoManageSys.Areas.Filters;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.DTO;
using Models;

namespace DemoManageSys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyFilter]
    public class MenuInfoController : Controller
    {
        private IMenuInfoBLL _menuInfoBLL;

        public MenuInfoController(
            IMenuInfoBLL menuInfoBLL
            )
        {
            _menuInfoBLL = menuInfoBLL;
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
        #endregion

        #region 获取样式表的方法 (GetMenuInfoes)
        /// <summary>
        /// 获取样式表的方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuInfoes(int page, int limit, string title)
        {
            int count;
            List<GetMenuInfoDTO> getMenuInfoDTOs = _menuInfoBLL.GetMenuInfoes(page, limit, title, out count);
            ReturnResule result = new ReturnResule()
            {
                Code = 0,
                Msg = "获取成功",
                Data = getMenuInfoDTOs,
                Count = count
            };

            return Json(result);
        }
        #endregion

        #region 添加的方法 (CreateMenuInfo)
        /// <summary>
        /// 添加的方法
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public IActionResult CreateMenuInfo([FromBody] MenuInfo menu)
        {
            string msg;
            ReturnResule resule = new ReturnResule();

            resule.IsSuccess = _menuInfoBLL.CreateMenuInfo(menu, out msg);
            if (resule.IsSuccess)
            {
                resule.Msg = msg;
                resule.Code = 200;
            };
            return Json(resule);
        }
        #endregion

        #region 删除的方法 (DeleteMenuInfo)
        /// <summary>
        ///删除的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteMenuInfo(string id)
        {
            ReturnResule result = new ReturnResule();
            result.IsSuccess = _menuInfoBLL.DeleteMenuInfo(id);
            if (result.IsSuccess)
            {
                result.Msg = "删除成功";
                result.Code = 200;
            }
            else
            {
                result.Msg = "删除失败";

            }
            return Json(result);
        }
        #endregion

        #region 修改的方法 (UpdateMenuInfo)
        /// <summary>
        /// 修改的方法
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateMenuInfo([FromBody] MenuInfo menu)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();

            string msg;//定义返回消息
            resule.IsSuccess = _menuInfoBLL.UpdateMenuInfo(menu, out msg);//调用方法传值

            resule.Msg = msg;
            if (resule.IsSuccess)
            {
                resule.Code = 200;
            }

            return Json(resule);
        }
        #endregion

        #region 批量软删除菜单的方法 (DeleteListMenuInfo)
        /// <summary>
        /// 批量软删除菜单的方法
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteListMenuInfo(List<string> ids)
        {
            //实例化对象
            ReturnResule resule = new ReturnResule();
            if (ids.Count == 0)
            {
                resule.Msg = "你没有返回任何用户";
                return Json(resule);
            }
            bool isOK = _menuInfoBLL.DeleteListMenuInfo(ids);//调用方法传值
            resule.Code = 200;
            resule.IsSuccess = isOK;
            resule.Msg = "删除用户成功";
            return Json(resule);
        }
        #endregion

        #region 根据ID获取下拉框 (GetMenuInfoById)
        /// <summary>
        /// 根据ID获取下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuInfoById(string id)
        {
            ReturnResule resule = new ReturnResule();

            if (string.IsNullOrWhiteSpace(id))
            {
                resule.Msg = "ID为空";
                return Json(resule);
            }

            MenuInfo menu = _menuInfoBLL.GetMenuInfoById(id);
            if (menu == null)
            {
                resule.Msg = "未获取到信息";
            }
            else
            {
                var selectOption = _menuInfoBLL.GetSelectOption();
                resule.Msg = "成功";
                resule.Code = 200;
                resule.Data = new
                {
                    menu = menu,//菜单信息
                    SelectOption = selectOption//下拉列表数据
                };
            }
            return Json(resule);
        }
        #endregion

        #region 获取下拉框的方法 (GetSelectOption)
        /// <summary>
        /// 获取下拉框的方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSelectOption()
        {
            ReturnResule resule = new ReturnResule();
            var data = _menuInfoBLL.GetSelectOption();

            if (data != null)
            {
                resule.Msg = "返回成功";
                resule.Data = data;
                resule.Code = 200;
            }
            return Json(resule);
        }
        #endregion

        #region 用户获取菜单列表的接口 (GetMenus)
        [HttpGet]
        public IActionResult GetMenus()
        {
            //登录用户ID
            var userId = HttpContext.Session.GetString("UserId");

            GetMenusDTO resule = new GetMenusDTO(new List<HomeMenuInfoDTO>());
   
            if(userId != null)
            {
                List<HomeMenuInfoDTO> menus = _menuInfoBLL.GetMenus(userId);

                resule = new GetMenusDTO(menus);
            }
            else
            {
                return Json(resule);
            }
            return Json(resule);
        }
        #endregion
    }
}