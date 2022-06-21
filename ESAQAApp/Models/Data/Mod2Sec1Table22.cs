using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod2Sec1Table22
    {
        [Key]
        public int Mod2Sec1Table22Id { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdAgriLandType22Id { get; set; }        
        public decimal? AmountCultiAgriLand { get; set; }        
    }
}