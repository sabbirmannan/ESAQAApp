using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec1Table112
    {
        [Key]
        public int Mod1Sec1Table112Id { get; set; }
        public int MasterDataId { get; set; }
        public int AgriNonagriAsset112Id { get; set; }

        [StringLength(250)]
        public string Others { get; set; }

        public decimal? ItemNumber { get; set; }
        public decimal? ItemPresentValue { get; set; }
    }
}