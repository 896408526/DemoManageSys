using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class OrderInfo : BaseEntity
    {
        /// <summary>
        /// 顾客ID
        /// </summary>
        [MaxLength(36)]
        public int CustomerID { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public int OrderAmount { get; set; }
    }
}
