using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod3Table35
    {
        [Key]
        public int Mod3Table35Id { get; set; }
        public int MasterDataId { get; set; }
        public int Mod3IrrigatedAgriLandType35Id { get; set; } //LookupMod3IrrigatedAgriLandType35
        public int? HasIrrigation { get; set; } //1 = yes; 0 = no

        public int? DeepTubeWellFuelCode35Id { get; set; } //LookupMod3FuelCode35
        public int? ShallowTubeWellFuelCode35Id { get; set; } //LookupMod3FuelCode35
        public int? PowerPumpTubeWellFuelCode35Id { get; set; } //LookupMod3FuelCode35
        public int? IrrigationDrainFuelCode35Id { get; set; } //LookupMod3FuelCode35
        public int? IndigenousMethodFuelCode35Id { get; set; } //LookupMod3FuelCode35
    }
}