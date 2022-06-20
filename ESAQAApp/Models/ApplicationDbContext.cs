using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using BAC007.Models.Data;
using BAC007.Models.TableLookup;

namespace BAC007.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        new public virtual IDbSet<ApplicationRole> Roles { get; set; }
        public virtual IDbSet<Group> Groups { get; set; }

        public ApplicationDbContext() : base("CEGIS-BAC007-DatabaseConnection")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Division> Division { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Upazila> Upazila { get; set; }
        public DbSet<Union> Union { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<AspNetUserMirror> AspNetUserMirror { get; set; }

        #region Khana Survey - Data
        public DbSet<Master> Master { get; set; }
        public DbSet<Mod1Sec1Detail> Mod1Sec1Detail { get; set; }
        public DbSet<Mod1Sec1Table110> Mod1Sec1Table110 { get; set; }
        public DbSet<Mod1Sec1Table111> Mod1Sec1Table111 { get; set; }
        public DbSet<Mod1Sec1Table112> Mod1Sec1Table112 { get; set; }
        public DbSet<Mod1Sec1Table115> Mod1Sec1Table115 { get; set; }
        public DbSet<Mod1Sec1Table116> Mod1Sec1Table116 { get; set; }
        public DbSet<Mod1Sec1Table117> Mod1Sec1Table117 { get; set; }
        public DbSet<Mod1Sec2Table119> Mod1Sec2Table119 { get; set; }
        public DbSet<Mod1Sec5Table137> Mod1Sec5Table137 { get; set; }

        public DbSet<Mod1Sec2Detail> Mod1Sec2Detail { get; set; }
        public DbSet<Mod1Sec3Detail> Mod1Sec3Detail { get; set; }
        public DbSet<Mod1Sec4Detail> Mod1Sec4Detail { get; set; }


        public DbSet<Mod2Sec1Table21> Mod2Sec1Table21 { get; set; }
        public DbSet<Mod2Sec1Table22> Mod2Sec1Table22 { get; set; }
        public DbSet<Mod2Sec1Table23> Mod2Sec1Table23 { get; set; }
        public DbSet<Mod2Sec1Table24> Mod2Sec1Table24 { get; set; }
        public DbSet<Mod2Sec1Table25> Mod2Sec1Table25 { get; set; }
        public DbSet<Mod2Sec2Table27> Mod2Sec2Table27 { get; set; }
        public DbSet<Mod2Sec2Table28> Mod2Sec2Table28 { get; set; }
        public DbSet<Mod2Sec3Table29> Mod2Sec3Table29 { get; set; }

        public DbSet<Mod3Table31> Mod3Table31 { get; set; }
        public DbSet<Mod3Table32> Mod3Table32 { get; set; }
        public DbSet<Mod3Table33> Mod3Table33 { get; set; }
        public DbSet<Mod3Table35> Mod3Table35 { get; set; }
        public DbSet<Mod3Table36> Mod3Table36 { get; set; }
        public DbSet<Mod3Table38> Mod3Table38 { get; set; }
        #endregion

        #region Khana Survey - Lookup
        public DbSet<LookupEducationLevel> LookupEducationLevel { get; set; }
        public DbSet<LookupMaritalStatus> LookupMaritalStatus { get; set; }
        public DbSet<LookupMovablePropertyOption> LookupMovablePropertyOption { get; set; }
        public DbSet<LookupFurnitureOther111> LookupFurnitureOther111 { get; set; }
        public DbSet<LookupAgriNonagriAsset112> LookupAgriNonagriAsset112 { get; set; }
        public DbSet<LookupGrossHouseholdIncome115> LookupGrossHouseholdIncome115 { get; set; }
        public DbSet<LookupFoodConsumpExps116> LookupFoodConsumpExps116 { get; set; }
        public DbSet<LookupStatementOfExpenditure117> LookupStatementOfExpenditure117 { get; set; }
        public DbSet<LookupTrainingOrganization122> LookupTrainingOrganization122 { get; set; }
        public DbSet<LookupAgriTrainingList127n129> LookupAgriTrainingList127n129 { get; set; }
        public DbSet<LookupHouseStructureType131> LookupHouseStructureType131 { get; set; }
        public DbSet<LookupDrinkingWaterSource133> LookupDrinkingWaterSource133 { get; set; }
        public DbSet<LookupSanitaryType135> LookupSanitaryType135 { get; set; }
        public DbSet<LookupSickList136> LookupSickList136 { get; set; }
        public DbSet<LookupTreatmentPlace136> LookupTreatmentPlace136 { get; set; }
        public DbSet<LookupLoanSource119> LookupLoanSource119 { get; set; }
        public DbSet<LookupUseOfLoanCode119> LookupUseOfLoanCode119 { get; set; }
        public DbSet<LookupWorkForLivingJobType137> LookupWorkForLivingJobType137 { get; set; }
        public DbSet<LookupMod2HouseholdAgriLand21> LookupMod2HouseholdAgriLand21 { get; set; }
        public DbSet<LookupMod2HouseholdAgriLandType22> LookupMod2HouseholdAgriLandType22 { get; set; }
        public DbSet<LookupMod2HouseholdLandType> LookupMod2HouseholdLandType { get; set; }
        public DbSet<LookupMod2HouseholdCropCode> LookupMod2HouseholdCropCode { get; set; }
        public DbSet<LookupMod2WaterSourceCode> LookupMod2WaterSourceCode { get; set; }
        public DbSet<LookupMod2IrrigationSysCode> LookupMod2IrrigationSysCode { get; set; }
        public DbSet<LookupMod2AvailabilityCode> LookupMod2AvailabilityCode { get; set; }
        public DbSet<LookupMod2StoreOfProdGoods> LookupMod2StoreOfProdGoods { get; set; }
        public DbSet<LookupMod2CropProcessing> LookupMod2CropProcessing { get; set; }
        public DbSet<LookupMod2CropMarketing> LookupMod2CropMarketing { get; set; }
        public DbSet<LookupMod2ProdDamageCode> LookupMod2ProdDamageCode { get; set; }
        public DbSet<LookupMod2ProdDamageReasonCode> LookupMod2ProdDamageReasonCode { get; set; }


        public DbSet<LookupMod3TypeOfChanges31> LookupMod3TypeOfChanges31 { get; set; }
        public DbSet<LookupMod3ImpactOfSubProject31> LookupMod3ImpactOfSubProject31 { get; set; }

        public DbSet<LookupMod3NaturalDisaster32> LookupMod3NaturalDisaster32 { get; set; }
        public DbSet<LookupMod3Recurrence32> LookupMod3Recurrence32 { get; set; }
        public DbSet<LookupMod3Extension32> LookupMod3Extension32 { get; set; }
        public DbSet<LookupMod3Dimension32> LookupMod3Dimension32 { get; set; }
        public DbSet<LookupMod3DrySeaWaterMngLandType33> LookupMod3DrySeaWaterMngLandType33 { get; set; }
        public DbSet<LookupMod3IrrigatedAgriLandType35> LookupMod3IrrigatedAgriLandType35 { get; set; }
        public DbSet<LookupMod3FuelCode35> LookupMod3FuelCode35 { get; set; }
        public DbSet<LookupMod3HouseholdWaterUse36> LookupMod3HouseholdWaterUse36 { get; set; }
        public DbSet<LookupMod3HouseholdWaterSource36> LookupMod3HouseholdWaterSource36 { get; set; }
        public DbSet<LookupMod3HouseholdOwnershipCode36> LookupMod3HouseholdOwnershipCode36 { get; set; }
        public DbSet<LookupMod3HouseholdWaterProperties36> LookupMod3HouseholdWaterProperties36 { get; set; }
        public DbSet<LookupMod3HouseholdWaterArsenic36> LookupMod3HouseholdWaterArsenic36 { get; set; }
        public DbSet<LookupMod3CurrentStatusSubProj38> LookupMod3CurrentStatusSubProj38 { get; set; }
        public DbSet<LookupMod3CurrentStatus38> LookupMod3CurrentStatus38 { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            // Keep this:
            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

            // Change TUser to ApplicationUser everywhere else - IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
            EntityTypeConfiguration<ApplicationUser> table =
                modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            table.Property((ApplicationUser u) => u.UserName).IsRequired();

            // EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
            modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");


            // Add the group stuff here:
            modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserGroup>((ApplicationUser u) => u.Groups);
            modelBuilder.Entity<ApplicationUserGroup>().HasKey((ApplicationUserGroup r) => new { UserId = r.UserId, GroupId = r.GroupId }).ToTable("ApplicationUserGroups");

            // And here:
            modelBuilder.Entity<Group>().HasMany<ApplicationRoleGroup>((Group g) => g.Roles);
            modelBuilder.Entity<ApplicationRoleGroup>().HasKey((ApplicationRoleGroup gr) => new { RoleId = gr.RoleId, GroupId = gr.GroupId }).ToTable("ApplicationRoleGroups");

            // And Here:
            EntityTypeConfiguration<Group> groupsConfig = modelBuilder.Entity<Group>().ToTable("Groups");
            groupsConfig.Property((Group r) => r.Name).IsRequired();

            // Leave this alone:
            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new { UserId = l.UserId, LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey }).ToTable("AspNetUserLogins");

            entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
            EntityTypeConfiguration<IdentityUserClaim> table1 = modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
            table1.HasRequired<IdentityUser>((IdentityUserClaim u) => u.User);

            // Add this, so that IdentityRole can share a table with ApplicationRole:
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 = modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();
        }
    }
}