using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserInfo : BaseDeleteEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [MaxLength(16)]
        public string Account { get; set; } = "";
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(16)]
        public string UserName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(32)]
        public string UserPwd { get; set; } = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(32)]
        public string Eamil { get; set; } = "";
        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(16)]
        public string Phone { get; set; } = "";
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}