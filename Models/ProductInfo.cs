using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 商品表
    /// </summary>
    public class ProductInfo : BaseDeleteEntity
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [MaxLength(32)]
        public string ProductName { get; set; } = "";
        /// <summary>
        /// 商品价格
        /// </summary>
        public int ProductPrice { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int ProductNum { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        [MaxLength(64)]
        public string Imgage { get; set; } = "";
        /// <summary>
        /// 商品分类ID
        /// </summary>
        [MaxLength(36)]
        public string CategorylD { get; set; }
    }
}
