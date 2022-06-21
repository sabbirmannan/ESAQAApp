using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod2StoreOfProdGoods
    {
        [Key]
        public int StoreOfProdGoodsId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}