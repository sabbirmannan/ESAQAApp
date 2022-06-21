namespace BAC007.Models.Master
{
    public class EsaMaster
    {
        [Key]
        public int MasterDataId { get; set; }

        [StringLength(50)]
        [Display(Name = "Name of the Respondent ")]
        public string RespondentName { get; set; }

        [StringLength(10)]
        [Display(Name = "Date")]
        public string FormDate { get; set; }
    }
}