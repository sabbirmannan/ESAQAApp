using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models.LookUpTable
{
    public class LkpDegradationCauseQa2
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
