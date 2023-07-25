using System;
using System.Collections.Generic;
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
        public int CustomerID { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public int OrderAmount { get; set; }
    }
}
