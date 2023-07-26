using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(36)]
        public string CustomerId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        [MaxLength(36)]
        public string ProductId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        [MaxLength(36)]
        public string OrderId { get; set; }

    }
}
