using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    public class R_RoleInfo_MenuInfo : BaseEntity
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [MaxLength(36)]
        public string MenuId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [MaxLength(36)]
        public string RoleId { get; set; }
    }
}
