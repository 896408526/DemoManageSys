using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 订单明细表
    /// </summary>
    public class OrderDetail : BaseEntity
    {
        /// <summary>
        /// 订单ID 
        /// </summary>
        [MaxLength(36)]
        public string OrderId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        [MaxLength(36)]
        public string ProductlD { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNum { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public int ProductPrice { get; set; }
    }
}
