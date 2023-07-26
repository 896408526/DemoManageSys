using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 进货明细表
    /// </summary>
    public class PurchaseDetail : BaseEntity
    {
        /// <summary>
        /// 进货ID
        /// </summary>
        [MaxLength(36)]
        public string PurchaseId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        [MaxLength(36)]
        public string ProductId { get; set; }
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
