using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 顾客表
    /// </summary>
    public class CustomerInfo : BaseDeleteEntity
    {
        /// <summary>
        /// 顾客姓名
        /// </summary>
        [MaxLength(32)]
        public string CustomerName { get; set; } = "";
        /// <summary>
        /// 顾客地址Id
        /// </summary>
        public int AddressId { get; set; }

    }
}
