using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 进货表
    /// </summary>
    public class Purchase : BaseDeleteEntity
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        [MaxLength(36)]
        public string SupplierId { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        [MaxLength(36)]
        public string PurchaseName { get; set; } = "";
        /// <summary>
        /// 库存
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 进货总金额
        /// </summary>
        public int PurchaseAmount { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(36)]
        public string Description { get; set; } = "";
    }
}
