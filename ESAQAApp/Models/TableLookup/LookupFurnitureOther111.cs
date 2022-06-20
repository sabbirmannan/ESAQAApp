using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupFurnitureOther111
    {
        [Key]
        public int LookupFurnitureOther111Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}