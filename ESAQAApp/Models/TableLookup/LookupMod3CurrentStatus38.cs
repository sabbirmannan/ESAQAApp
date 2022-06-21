using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models.TableLookup
{
    public class LookupMod3CurrentStatus38
    {
        [Key]
        public int CurrentStatus38Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}