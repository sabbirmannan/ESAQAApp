using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models.LookUpTable
{
    public class LkpDegradationLevelQa2
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
