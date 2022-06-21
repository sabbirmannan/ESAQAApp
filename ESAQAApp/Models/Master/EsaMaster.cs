using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models.Master
{
    public class EsaMaster
    {
        [Key]
        public int MasterDataId { get; set; }

        [StringLength(50)]
        public string RespondentName { get; set; }

        [StringLength(10)]
        public string FormDate { get; set; }

        public int ModuleADetailId { get; set; }
        public int TypeOfWetLandQa1Id { get; set; }
        public int TypeOfEcoSysQa3Id { get; set; }
    }
}
