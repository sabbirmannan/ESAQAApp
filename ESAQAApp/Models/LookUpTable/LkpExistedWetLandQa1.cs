using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models.LookUpTable
{
    public class LkpExistedWetLandQa1
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
