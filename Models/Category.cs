using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 商品分类表
    /// </summary>
    public class Category : BaseDeleteEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(32)]
        public string CategoryName { get; set; } = "";
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(32)]
        public string Description { get; set; } = "";
    }
}
