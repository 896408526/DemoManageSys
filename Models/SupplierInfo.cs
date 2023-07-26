using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 供应商表
    /// </summary>
    public class SupplierInfo :BaseDeleteEntity
    {
        /// <summary>
        /// 供应商名称
        /// </summary>
        [MaxLength(36)]
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商联系人
        /// </summary>
        [MaxLength(36)]
        public string SupplierContact { get; set; }

        /// <summary>
        /// 供应商电话号码
        /// </summary>
        [MaxLength(36)]
        public string SupplierPhone { get; set; }
    }
}
