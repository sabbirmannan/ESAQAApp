using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupFoodConsumpExps116
    {
        [Key]
        public int FoodConsumpExps116Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}