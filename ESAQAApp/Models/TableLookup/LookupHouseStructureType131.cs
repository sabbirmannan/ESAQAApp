using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupHouseStructureType131
    {
        [Key]
        public int HouseStructureType131Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}