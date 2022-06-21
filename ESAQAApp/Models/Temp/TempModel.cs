using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESAQAApp.Models.Temp
{
    public class TempModel
    {

    }

    public class ReponseDataTemp
    {
        public int MasterDataId { get; set; }
        public int LookupDataId { get; set; }
        public string DataValue { get; set; }
    }


    public class GetMasterDataList
    {
        public int MasterDataId { get; set; }

        [Display(Name = "কেস নং")]
        public string CaseNo { get; set; }

        [Display(Name = "তারিখ")]
        public string FormDate { get; set; }

        [Display(Name = "সাক্ষাৎকার গ্রহণের স্থান")]
        public string PlaceOfInterview { get; set; }

        [Display(Name = "জিপিএস লোকেশন")]
        public decimal? LocLatDecimal { get; set; }

        [Display(Name = "জিপিএস লোকেশন")]
        public decimal? LocLongDecimal { get; set; }

        [Display(Name = "উত্তরদাতার ধরণ")]
        public string TypeOfRespondent { get; set; }

        [Display(Name = "বিভাগ")]
        public string DivisionName { get; set; }

        [Display(Name = "জেলা")]
        public string DistrictName { get; set; }

        [Display(Name = "উপজেলা")]
        public string UpazilaName { get; set; }

        [Display(Name = "ইউনিয়ন")]
        public string UnionName { get; set; }

        [Display(Name = "গ্রাম")]
        public string Village { get; set; }

        [Display(Name = "পাড়া")]
        public string Para { get; set; }

        [Display(Name = "ওয়ার্ড নং")]
        public string WordNo { get; set; }

        [Display(Name = "বাড়ী নং (যদি থাকে)")]
        public string HouseNo { get; set; }

        [Display(Name = "সাক্ষাৎকার গ্রহণকারীর নাম")]
        public string NameOfInterviewer { get; set; }

        [Display(Name = "সুপারভাইজার/ কোয়ালিটি কন্ট্রোল অফিসারের নাম")]
        public string NameOfSupervisor { get; set; }

        [StringLength(10)]
        [Display(Name = "তারিখ")]
        public string DataDate { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class GetMod1Sec1Table110DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int MovablePropertyOptionId { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? LandDecimal { get; set; }
        public decimal? LandPresentValue { get; set; }
    }

    public class GetMod1Sec1Table111DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int FurnitureOtherMaterialOptionId { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? ItemNumber { get; set; }
        public decimal? ItemPresentValue { get; set; }
    }

    public class GetMod1Sec1Table112DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int AgriNonAgriAssetOtherOptionId { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? ItemNumber { get; set; }
        public decimal? ItemPresentValue { get; set; }
    }

    public class GetMod1Sec1Table115DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int GrossHouseholdOtherIncomeId { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public decimal? YearlyIncome { get; set; }
    }

    public class GetMod1Sec1Table116DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int FoodConsExpOtherOptionId { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? AmountOfConsumption { get; set; }
        public decimal? ConsumptionValue { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class GetMod1Sec1Table117DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int StatementOfExpenditureOptionId { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class GetMod1Sec2Table119DataTemp
    {
        public int RowId { get; set; }
        public Int32 MasterDataId { get; set; }
        public int LoanSource119Id { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public decimal? LoanAmount { get; set; }
        public int? UseOfLoanCode119Id { get; set; }
        public string LoanCodeOptionName { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public decimal? AvgInterest { get; set; }
    }

    public class GetMod1Sec5Table137DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int WorkForLivingJobType137Id { get; set; }
        public string OptionName { get; set; }
        public string Others { get; set; }
        public int? TotalDays { get; set; }
        public int? AvgHour { get; set; }
        public decimal? ApproxEarnLastYear { get; set; }
    }

    public class GetMod2Sec1Table21DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdAgriLand21Id { get; set; }
        public string OptionName { get; set; }
        public decimal? TotalLand { get; set; }
        public decimal? Crop_1 { get; set; }
        public decimal? Crop_2 { get; set; }
        public decimal? Crop_3 { get; set; }
    }

    public class GetMod2Sec1Table22DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdAgriLandType22Id { get; set; }
        public string OptionName { get; set; }
        public decimal? AmountCultiAgriLand { get; set; }
    }

    public class GetMod2Sec1Table23DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdLandTypeId { get; set; }
        public string OptionName { get; set; }
        public int CropCodeId { get; set; }
        public string CropCodeName { get; set; }
        public string OtherCropCode { get; set; }
        public decimal? LandAmntIrrigatedLand { get; set; }
        public decimal? LandAmntWithoutIrrigation { get; set; }
        public decimal? CropProdAmntIrrigatedLand { get; set; }
        public decimal? CropProdAmntWithoutIrrigation { get; set; }
        public decimal? TotalValueCropReceived { get; set; }
        public decimal? TotalValueByproducts { get; set; }
        public decimal? TotalCropYield { get; set; }
        public decimal? ShareholderShare { get; set; }
    }

    public class GetMod2Sec1Table24DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int T24HouseholdLandTypeId { get; set; }
        public string OptionName { get; set; }
        public int T24CropCodeId { get; set; }
        public string CropCodeName { get; set; }
        public string OtherT24CropCode { get; set; }

        public decimal? UreaAmount { get; set; }
        public decimal? UreaValue { get; set; }
        public decimal? PotashTspAmount { get; set; }
        public decimal? PotashTspValue { get; set; }
        public decimal? PesticidesAmount { get; set; }

        public decimal? TotalCostIrrigation { get; set; }
        public decimal? SeedsAmount { get; set; }
        public decimal? SeedsValue { get; set; }

        public decimal? PowerTillerTaka { get; set; }

        public int? SelfEmployedLaborDays { get; set; }
        public int? RentLaborDays { get; set; }
        public decimal? DailyLaborCost { get; set; }
        public decimal? TotalCost { get; set; }
    }

    public class GetMod2Sec1Table25DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public int T25CropCodeId { get; set; }
        public string OptionName { get; set; }
        public int WaterSourceCodeId { get; set; }
        public string WaterSourceOptionName { get; set; }
        public int IrrigationSysCodeId { get; set; }
        public string IrrigationSysOptionName { get; set; }
        public int AvailabilityCodeId { get; set; }
        public string AvailabilityOptionName { get; set; }
    }

    public class GetMod2Sec2Table27DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public string OptionName { get; set; }
        public string CropProcessingOptionName { get; set; }
        public string CropDryProcessingOptionName { get; set; }
        public string CropStoreProcessingOptionName { get; set; }
        public string CropMarketingOptionName { get; set; }
    }

    public class GetMod2Sec2Table28DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public string OptionName { get; set; }

        public decimal? TotalCropProduction { get; set; }
        public decimal? TotalCropProdSelfUse { get; set; }
        public decimal? RestMarketCropProd { get; set; }
        public decimal? FieldSaleCropProdAmount { get; set; }
        public decimal? FieldSaleCropProdValue { get; set; }
        public string MarketName { get; set; }

        public decimal? DistanceOfLandToMarket { get; set; }
        public decimal? TravelCostPerMon { get; set; }
        public decimal? TotalCost { get; set; }
    }

    public class GetMod2Sec3Table29DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }
        public string OptionName { get; set; }

        public string ProdDamageCodeName { get; set; }
        public string ProdDamageReasonCodeName { get; set; }
    }

    public class GetMod3Table31DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }

        public string Mod3TypeOfChanges31Name { get; set; }
        public decimal? PresentCondition { get; set; }
        public string Mod3ImpactOfSubProject31Name { get; set; }
    }

    public class GetMod3Table33DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }

        public string Mod3DrySeaWaterMngLandType33Name { get; set; }
        public decimal? AmountCultivatedAgriLand { get; set; }
    }

    public class GetMod3Table35DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }

        public string Mod3IrrigatedAgriLandType35Name { get; set; } //LookupMod3IrrigatedAgriLandType35
        public string HasIrrigation { get; set; } //1 = yes; 0 = no
        public string DeepTubeWellFuelCode35Name { get; set; } //LookupMod3FuelCode35
        public string ShallowTubeWellFuelCode35Name { get; set; } //LookupMod3FuelCode35
        public string PowerPumpTubeWellFuelCode35Name { get; set; } //LookupMod3FuelCode35
        public string IrrigationDrainFuelCode35Name { get; set; } //LookupMod3FuelCode35
        public string IndigenousMethodFuelCode35Name { get; set; } //LookupMod3FuelCode35
    }

    public class GetMod3Table32DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }

        public string Mod3NaturalDisaster32Name { get; set; } 
        public string Mod3Recurrence32Name { get; set; } 
        public string Mod3Extension32Name { get; set; } 
        public string Mod3Dimension32Name { get; set; } 
    }

    public class GetMod3Table36DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }

        public string HouseholdWaterUse36Name { get; set; } 
        public string HouseholdWaterSource36Name { get; set; } 
        public string HouseholdOwnershipCode36Name { get; set; } 
        public string HouseholdWaterProperties36Name { get; set; } 
        public string HouseholdWaterArsenic36Name { get; set; } 
    }

    public class GetMod3Table38DataTemp
    {
        public int RowId { get; set; }
        public int MasterDataId { get; set; }

        public string CurrentStatusSubProj38Name { get; set; }
        public string CurrentStatus38Name { get; set; }
        public string NeedRepairDigCanalName { get; set; }
    }
}