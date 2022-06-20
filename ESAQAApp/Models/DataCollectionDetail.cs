using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DataCollectionDetail
    {
        [Key]
        public int DataCollectionDetailID { get; set; }

        [ForeignKey("DataCollectionMaster")]
        [Required(ErrorMessage = "Data Collection Master ID is empty!")]
        public Int32 DataCollectionMasterID { get; set; }
        public virtual DataCollectionMaster DataCollectionMaster { get; set; }

        [Display(Name = "AnySignificantEnvInterest1_1", ResourceType = typeof(Resources.Resources))]
        public bool? AnySignificantEnvInterest1_1 { get; set; }

        [Display(Name = "TypeOfEnvSignificance1_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(200, ErrorMessage = "Information cannot be longer than 200 characters.")]
        public string TypeOfEnvSignificance1_2 { get; set; }

        [Display(Name = "DescOfEnvSignificance1_3", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfEnvSignificance1_3 { get; set; }

        [Display(Name = "IsThereAnyArcheologicalPlace2_1", ResourceType = typeof(Resources.Resources))]
        public bool? IsThereAnyArcheologicalPlace2_1 { get; set; }

        [Display(Name = "DescOfImpactPollutedWater2_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfImpactPollutedWater2_2 { get; set; }

        [Display(Name = "AnyHabitatNearSampleLocation3_1", ResourceType = typeof(Resources.Resources))]
        public bool? AnyHabitatNearSamplingLocation3_1 { get; set; }

        [Display(Name = "PopulationStatus3_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(25, ErrorMessage = "Information cannot be longer than 25 characters.")]
        public string PopulationStatus3_2 { get; set; }

        [Display(Name = "NearbyPolutionSource3_3", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string NearbyPolutionSource3_3 { get; set; }

        [Display(Name = "SourceDesc3_4", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string SourceDesc3_4 { get; set; }

        [Display(Name = "UsesOfWater3_5", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string UsesOfWater3_5 { get; set; }

        [Display(Name = "DescOfImpactUsesWater3_6", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfImpactUsesWater3_6 { get; set; }

        [Display(Name = "IsSolelyDependencyOnWater4_1", ResourceType = typeof(Resources.Resources))]
        public bool? IsSolelyDependencyOnWater4_1 { get; set; }

        [Display(Name = "DescOfDependency4_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfDependency4_2 { get; set; }

        [Display(Name = "IsUsedForIrrigation5", ResourceType = typeof(Resources.Resources))]
        public bool? IsUsedForIrrigation5 { get; set; }

        [Display(Name = "IsAnyPlantForRiverWaterTreatment6", ResourceType = typeof(Resources.Resources))]
        public bool? IsAnyPlantForRiverWaterTreatment6 { get; set; }

        [Display(Name = "IsAnyPollutionSpot7_1", ResourceType = typeof(Resources.Resources))]
        public bool? IsAnyPollutionSpot7_1 { get; set; }

        [Display(Name = "TypeOfThePollution7_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string TypeOfThePollution7_2 { get; set; }

        [Display(Name = "DescTheTypeOfPollutantsSpot7_3", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescTheTypeOfPollutantsSpot7_3 { get; set; }

        [Display(Name = "IsThereAnyDenseZone8_1", ResourceType = typeof(Resources.Resources))]
        public bool? IsThereAnyDenseZone8_1 { get; set; }

        [Display(Name = "TypeOfIndustry8_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Information cannot be longer than 100 characters.")]
        public string TypeOfIndustry8_2 { get; set; }

        [Display(Name = "DescWateManage8_3", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescWateManage8_3 { get; set; }

        [Display(Name = "TypeOfWasteDischarge8_4", ResourceType = typeof(Resources.Resources))]
        [StringLength(25, ErrorMessage = "Information cannot be longer than 25 characters.")]
        public string TypeOfWasteDischarge8_4 { get; set; }

        [Display(Name = "DistanceBetIndustryAndHabitat9", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string DistanceBetIndustryAndHabitat9 { get; set; }

        [Display(Name = "FlowDirectionOfRiver10", ResourceType = typeof(Resources.Resources))]
        [StringLength(25, ErrorMessage = "Information cannot be longer than 25 characters.")]
        public string FlowDirectionOfRiver10 { get; set; }

        [Display(Name = "HowDwellersUseRiverWater11_1", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string HowDwellersUseRiverWater11_1 { get; set; }

        [Display(Name = "DescOfHowDwellersUse11_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfHowDwellersUse11_2 { get; set; }

        [Display(Name = "IsThereAnyWasteDrain12_1", ResourceType = typeof(Resources.Resources))]
        public bool? IsThereAnyWasteDrain12_1 { get; set; }

        [Display(Name = "TypeOfDrainageOutfall12_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(200, ErrorMessage = "Information cannot be longer than 200 characters.")]
        public string TypeOfDrainageOutfall12_2 { get; set; }

        [Display(Name = "IsThereAnyCommunicationSite13_1", ResourceType = typeof(Resources.Resources))]
        public bool? IsThereAnyCommunicationSite13_1 { get; set; }

        [Display(Name = "AnyProblemFacedByPeople13_2", ResourceType = typeof(Resources.Resources))]
        public bool? AnyProblemFacedByPeople13_2 { get; set; }

        [Display(Name = "DescOfProblem13_3", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfProblem13_3 { get; set; }

        [Display(Name = "PrevUseOfWater14_1", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string PrevUseOfWater14_1 { get; set; }

        [Display(Name = "DescOfPrevUse14_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfPrevUse14_2 { get; set; }

        [Display(Name = "CurrentUseOfWater15_1", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Information cannot be longer than 150 characters.")]
        public string CurrentUseOfWater15_1 { get; set; }

        [Display(Name = "DescOfCurrentUse15_2", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Information cannot be longer than 500 characters.")]
        public string DescOfCurrentUse15_2 { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool? IsActive { get; set; }

        [Display(Name = "IsDelete", ResourceType = typeof(Resources.Resources))]
        public bool? IsDelete { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Resources))]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy", ResourceType = typeof(Resources.Resources))]
        public string UpdateBy { get; set; }

        [Display(Name = "UpdatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "DeletedBy", ResourceType = typeof(Resources.Resources))]
        public string DeletedBy { get; set; }

        [Display(Name = "DeletedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? DeletedDate { get; set; }
    }
}