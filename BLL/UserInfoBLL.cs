﻿using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
using Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class UserInfoBLL : IUserInfoBLL
    {
        private IUserInfoDAL _userInfoDAL;
        private IDepartmentInfoDAL _departmentInfoDAL;
        public UserInfoBLL(
            IUserInfoDAL userInfoDAL
            , IDepartmentInfoDAL departmentInfoDAL
            )
        {
            //_userInfoDAL = new UserInfoDAL();//实例化
            _userInfoDAL = userInfoDAL;
            _departmentInfoDAL = departmentInfoDAL;
        }

        #region 登录的方法 (Login)
        /// <summary>
        /// 登录的方法
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="msg"></param>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool LoginIndex(string account, string userPwd, out string msg, out string userName, out string userId)
        {
            try
            {
                string newPasswork = MD5help.Md5(userPwd);
                UserInfo userInfo = _userInfoDAL.GetEntities().FirstOrDefault(it => it.Account == account && it.PassWord == newPasswork);

                userName = "";
                userId = "";
                if (userInfo == null)
                {
                    msg = "账号或者密码错误";
                    return false;
                }
                else
                {
                    msg = "成功";
                    userName = userInfo.UserName;
                    userId = userInfo.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                userName = "";
                userId = "";
                msg = "网络异常请检查网络" + ex;
                return false;
            }
        }
        #endregion

        #region 获取用户列表的方法 (GetUserInfoes)
        /// <summary>
        /// 获取用户列表的方法
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页几行</param>
        /// <param name="userName">姓名</param>
        /// <param name="account">账号</param>
        /// <param name="count">共多少条数据</param>
        /// <returns></returns>
        public List<GetUserInfoDTO> GetUserInfoes(int page, int limit, string userName, string account, out int count)
        {
            //Linq
            var data = from u in _userInfoDAL.GetEntities().Where(u => u.IsDelete == false)
                       join d in _departmentInfoDAL.GetEntities().Where(d => d.IsDelete == false)
                       on u.DepartmentId equals d.Id
                       into tempUD
                       from dd in tempUD.DefaultIfEmpty()
                       select new GetUserInfoDTO
                       {
                           Id = u.Id,
                           Account = u.Account,
                           UserName = u.UserName,
                           Phone = u.Phone,
                           Email = u.Email,
                           DepartmentId = u.DepartmentId,
                           DepartmentName = dd.DepartmentName,
                           Sex = u.Sex == 0 ? "女" : "男",
                           CreateTime = u.CreateTime
                       };

            if (!string.IsNullOrWhiteSpace(userName))
            {
                //contains()方法类似与模糊查询
                data = data.Where(u => u.UserName.Contains(userName));
            }
            if (!string.IsNullOrWhiteSpace(account))
            {
                //条件精准查询
                data = data.Where(u => u.Account == account);
            }
            //计算数据总数
            count = data.Count();
            //分页(降序)
            return data.OrderByDescending(u => u.CreateTime)
                                .Skip(limit * (page - 1))
                                .Take(limit)
                                .ToList();
        }
        #endregion

        #region 创建用户 (CreateUserInfo)
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public bool CreateUserInfo(UserInfo user, out string msg)
        {
            if (string.IsNullOrWhiteSpace(user.Account))
            {
                msg = "账号不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                msg = "用户名不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.PassWord))
            {
                msg = "密码不能为空";
                return false;
            }
            //验证用户账号是否存在
            if (_userInfoDAL.GetEntities().FirstOrDefault(it => it.Account == user.Account) != null)
            {
                msg = "账号已经存在";
                return false;
            }
            user.PassWord = MD5help.Md5(user.PassWord);//密码加密
            user.Id = Guid.NewGuid().ToString();//用户id
            user.CreateTime = DateTime.Now;//设置时间
            bool IsSuccess = _userInfoDAL.CreateEntity(user);//调用方法
            msg = IsSuccess ? $"添加{user.UserName}成功" : $"添加用户失败";

            return IsSuccess;
        }
        #endregion

        #region 删除的方法 (DeleteUserInfo)
        /// <summary>
        /// 删除的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUserInfo(string id)
        {
            UserInfo user = _userInfoDAL.GetEntityById(id);
            if (user == null)
            {
                return false;
            }
            user.IsDelete = true;
            user.DeleteTime = DateTime.Now;
            _userInfoDAL.UpdateEntity(user);
            return true;
        }
        #endregion

        #region 批量删除的方法 (DeleteListUserInfo)
        /// <summary>
        /// 批量删除的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteListUserInfo(List<string> ids)
        {
            List<UserInfo> userList = _userInfoDAL.GetEntities().Where(it => ids.Contains(it.Id)).ToList();
            foreach (var item in ids)
            {
                UserInfo user = _userInfoDAL.GetEntityById(item);
                if (userList == null)
                {
                    return false;
                }
                user.IsDelete = true;
                user.DeleteTime = DateTime.Now;

                _userInfoDAL.UpdateEntity(user);
            }
            return true;
        }
        #endregion

        #region 修改的方法 (UpdateUserInfo)
        /// <summary>
        /// 修改用户的方法
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserInfo user, out string msg)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
            {
                msg = $"id不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.Account))
            {
                msg = $"账号不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                msg = $"姓名不能为空";
                return false;
            }
            //查询用户是否存在
            UserInfo entity = _userInfoDAL.GetEntityById(user.Id);
            if (entity.Id == null)
            {
                msg = $"用户不存在";
                return false;
            }
            //判断重复
            if (entity.Account != user.Account)
            {
                var data = _userInfoDAL.GetEntities().FirstOrDefault(it => it.Account == user.Account);
                if (data != null)
                {
                    msg = "账号已经被占用";
                    return false;
                }
            }
            entity.Account = user.Account;
            entity.UserName = user.UserName;
            entity.Phone = user.Phone;
            entity.Email = user.Email;

            entity.PassWord = string.IsNullOrWhiteSpace(user.PassWord) ? entity.PassWord : MD5help.Md5(user.PassWord);

            bool IsSuccess = _userInfoDAL.UpdateEntity(entity);//调用方法

            msg = IsSuccess ? $"修改成功" : "修改用户失败";

            return IsSuccess;
        }
        #endregion

        #region 根据ID获取数据返回赋值 (GetUserInfoById)
        /// <summary>
        /// 根据ID获取数据返回赋值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoById(string id)
        {
            return _userInfoDAL.GetEntityById(id);
        }
        #endregion

        #region 账号密码修改 (UpdatePassWord)
        public bool UpdatePassWord(string id, string oldPwd, string newPwd, string agPwd, out string msg)
        {
            if (string.IsNullOrWhiteSpace(oldPwd))
            {
                msg = $"请填写密码";
                return false;
            }
            if (string.IsNullOrWhiteSpace(newPwd))
            {
                msg = $"新密码不能为空";
                return false;
            }
            if (newPwd != agPwd)
            {
                msg = $"新密码和确认密码必须一致";
                return false;
            }

            UserInfo user = _userInfoDAL.GetEntityById(id);
            if (user == null)
            {
                msg = $"用户查找失败";
                return false;
            }

            oldPwd = MD5help.Md5(oldPwd);
            newPwd = MD5help.Md5(newPwd);

            if (user.PassWord != oldPwd)
            {
                msg = $"旧密码输入错误";
                return false;
            }

            user.PassWord = newPwd;

            bool IsSuccess = _userInfoDAL.UpdateEntity(user);//调用方法

            msg = IsSuccess ? $"修改成功" : "修改密码失败";

            return IsSuccess;

        }
        #endregion

        #region 设置用户信息的方法 (SettingUser)
        /// <summary>
        /// 设置用户信息的方法
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SettingUser(UserInfo user, out string msg)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                msg = $"姓名不能为空";
                return false;
            }
            UserInfo entity = _userInfoDAL.GetEntityById(user.Id);
            if (entity.Id == null)
            {
                msg = $"用户不存在";
                return false;
            }

            entity.UserName = user.UserName;
            entity.Phone = user.Phone;
            entity.Email = user.Email;
            bool IsSuccess = _userInfoDAL.UpdateEntity(entity);//调用方法

            msg = IsSuccess ? $"修改成功" : "修改用户失败";

            return IsSuccess;
        }
        #endregion

        #region 获取全部用户的方法 (GetUserInfoDTOs)
        /// <summary>
        /// 获取全部用户的方法
        /// </summary>
        /// <returns></returns>
        public List<GetUserInfoDTO> GetUserInfoDTOs()
        {
            var data = _userInfoDAL.GetEntities()
                                    .Where(it => it.IsDelete == false)
                                    .Select(it => new GetUserInfoDTO
                                    {
                                        Id = it.Id,
                                        UserName = it.UserName
                                    }).ToList();

            return data;
        }
        #endregion
    }
}
