using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 购物车表
    /// </summary>
    public class ShopInfo : BaseEntity
    {
        /// <summary>
        /// 顾客ID
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderId { get; set; }

    }
}
