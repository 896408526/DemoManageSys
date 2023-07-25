using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 顾客地址表
    /// </summary>
    public class CustomerAddress : BaseEntity
    {
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(128)]
        public string Address { get; set; } = "";
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(16)]
        public string Phone { get; set; } = "";
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(32)]
        public string Decription { get; set; } = "";
    }
}
