using System.Data.Entity.Core.Objects;
using Detective.DataModel;

namespace Detective.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
    using System.Collections;
    public partial class BlackcheckEntities : DbContext
    {
        public BlackcheckEntities()
            : base("name=BlackcheckEntities")
        {

        }

        public virtual DbSet<SDNItem> T_SDN { get; set; }
        public virtual DbSet<AddressItem> T_ADDRESS_ITEM { get; set; }
        public virtual DbSet<AkaItem> T_AKA_ITEM { get; set; }
        public virtual DbSet<CitizenshipItem> T_CITIZENSHIP_ITEM { get; set; }
        public virtual DbSet<DateOfBirthItem> T_DATEOFBIRTH_ITEM { get; set; }
        public virtual DbSet<IDItem> T_ID_ITEM { get; set; }
        public virtual DbSet<NationalityItem> T_NATIONALITY_ITEM { get; set; }
        public virtual DbSet<PlaceOfBirthItem> T_PLACEOFBIRTH_ITEM { get; set; }
        public virtual DbSet<ProgramItem> T_PROGRAM_ITEM { get; set; }
        public virtual DbSet<VesselInfoItem> T_VESSELINFO_ITEM { get; set; }

        public class BulkCopyIndexHelper
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<AddressItem>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.address2)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.address3)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.state_or_province)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.postal_code)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<AddressItem>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<AkaItem>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<AkaItem>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<CitizenshipItem>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<DateOfBirthItem>()
                .Property(e => e.date_of_birth)
                .IsUnicode(false);

            modelBuilder.Entity<IDItem>()
                .Property(e => e.id_type)
                .IsUnicode(false);

            modelBuilder.Entity<IDItem>()
                .Property(e => e.id_number)
                .IsUnicode(false);

            modelBuilder.Entity<IDItem>()
                .Property(e => e.id_country)
                .IsUnicode(false);

            modelBuilder.Entity<IDItem>()
                .Property(e => e.issue_date)
                .IsUnicode(false);

            modelBuilder.Entity<NationalityItem>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceOfBirthItem>()
                .Property(e => e.place_of_birth)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramItem>()
                .Property(e => e.program)
                .IsUnicode(false);

            modelBuilder.Entity<SDNItem>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<SDNItem>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<SDNItem>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<SDNItem>()
                .Property(e => e.sdn_type)
                .IsUnicode(false);

            modelBuilder.Entity<SDNItem>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<VesselInfoItem>()
                .Property(e => e.call_sign)
                .IsUnicode(false);

            modelBuilder.Entity<VesselInfoItem>()
                .Property(e => e.vessel_type)
                .IsUnicode(false);

            modelBuilder.Entity<VesselInfoItem>()
                .Property(e => e.vessel_flag)
                .IsUnicode(false);

            modelBuilder.Entity<VesselInfoItem>()
                .Property(e => e.vessel_owner)
                .IsUnicode(false);*/


            modelBuilder.Entity<SDNItem>()
                .ToTable("T_SDN_ITEM");

            modelBuilder.Entity<AddressItem>()
                .ToTable("T_ADDRESS_ITEM");

            modelBuilder.Entity<AkaItem>()
                .ToTable("T_AKA_ITEM");

            modelBuilder.Entity<CitizenshipItem>()
                .ToTable("T_CITIZENSHIP_ITEM");


            modelBuilder.Entity<DateOfBirthItem>()
                .ToTable("T_DATEOFBIRTH_ITEM");

            modelBuilder.Entity<IDItem>()
                .ToTable("T_ID_ITEM");

            modelBuilder.Entity<NationalityItem>()
                .ToTable("T_NATIONALITY_ITEM");


            modelBuilder.Entity<PlaceOfBirthItem>()
                .ToTable("T_PLACEOFBIRTH_ITEM");


            modelBuilder.Entity<ProgramItem>()
                .ToTable("T_PROGRAM_ITEM");

            modelBuilder.Entity<VesselInfoItem>()
                .ToTable("T_VESSELINFO_ITEM");

            modelBuilder.Entity<SDNItem>()
                .HasMany(x => x.AddressList)
                .WithMany(x => x.SDNs)
                .Map(x => x.MapLeftKey("sdn_id")
                    .MapRightKey("address_id")
                    .ToTable("T_SDN_ADDRESS_ITEM"));


            modelBuilder.Entity<SDNItem>()
                .HasMany(x => x.NationalityList)
                .WithMany(x => x.SDNs)
                .Map(x => x.MapLeftKey("sdn_id")
                    .MapRightKey("nationality_id")
                    .ToTable("T_SDN_NATIONALITY"));

            modelBuilder.Entity<SDNItem>()
                .HasMany(x => x.NationalityList)
                .WithMany(x => x.SDNs)
                .Map(x => x.MapLeftKey("sdn_id")
                    .MapRightKey("nationality_id")
                    .ToTable("T_SDN_NATIONALITY"));

            modelBuilder.Entity<SDNItem>()
                .HasMany(x => x.CitizenshipList)
                .WithMany(x => x.SDNs)
                .Map(x => x.MapLeftKey("sdn_id")
                    .MapRightKey("citizenship_id")
                    .ToTable("T_SDN_CITIZENSHIP"));

            modelBuilder.Entity<SDNItem>()
                .HasMany(x => x.DateOfBirthItemList)
                .WithMany(x => x.SDNs)
                .Map(x => x.MapLeftKey("sdn_id")
                    .MapRightKey("dateofbirth_id")
                    .ToTable("T_SDN_DATEOFBIRTH"));


            modelBuilder.Entity<SDNItem>()
                .HasMany(x => x.PlaceOfBirthList)
                .WithMany(x => x.SDNs)
                .Map(x => x.MapLeftKey("sdn_id")
                    .MapRightKey("placeofbirth_id")
                    .ToTable("T_SDN_PLACEOFBIRTH"));


            modelBuilder.Entity<SDNItem>()
                 .HasMany(x => x.VesselInfoList)
                 .WithMany(x => x.SDNs)
                 .Map(x => x.MapLeftKey("sdn_id")
                     .MapRightKey("vesselinfo_id")
                     .ToTable("T_SDN_VESSELINFO"));

            modelBuilder.Entity<SDNItem>()
             .HasMany(x => x.ProgramList)
             .WithMany(x => x.SDNs)
             .Map(x => x.MapLeftKey("sdn_id")
                 .MapRightKey("program_id")
                 .ToTable("T_SDN_PROGRAM_ITEM"));

            modelBuilder.Entity<SDNItem>()
             .HasMany(x => x.IDList)
             .WithMany(x => x.SDNs)
             .Map(x => x.MapLeftKey("sdn_id")
                 .MapRightKey("id_id")
                 .ToTable("T_SDN_ID"));


            modelBuilder.Entity<SDNItem>()
             .HasMany(x => x.AkaList)
             .WithMany(x => x.SDNs)
             .Map(x => x.MapLeftKey("sdn_id")
                 .MapRightKey("aka_id")
                 .ToTable("T_SDN_AKA"));

        }

        #region operational & utils
        public int WipeOut()
        {
            var result = this.Database.ExecuteSqlCommand("usp_wipeout");
            return result;
        }

        public void BuildDataTable(DataTable dt, int targetListCount, string sdnColumn, string targetColumn, int sdnIndex, int targetIndex)
        {
            if (!dt.Columns.Contains(sdnColumn))
            {
                dt.Columns.Add(sdnColumn);
            }

            if (!dt.Columns.Contains(targetColumn))
            {
                dt.Columns.Add(targetColumn);
            }

            for (int i = 0; i < targetListCount; i++)
            {
                dt.Rows.Add(sdnIndex, targetIndex + i);
            }
        }

        public void BulkInsertCustom(DataTable dt, string destionationTable)
        {
            using (SqlBulkCopy blkcpy = new SqlBulkCopy(Database.Connection.ConnectionString))
            {
                blkcpy.DestinationTableName = destionationTable;
                blkcpy.EnableStreaming = true;
                blkcpy.WriteToServer(dt);
            }
        }

        public BlackcheckEntities BulkInsertCustom(IEnumerable<SDNItem> entityList, int savePerRecordCount)
        {
            BlackcheckEntities context = this;

            if (context == null || savePerRecordCount <= 0 || entityList == null)
            {
                throw new ArgumentException("Parameter(s) are invalid");
            }

            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            int counter = 1;

            foreach (var item in entityList)
            {
                Trace.WriteLine(String.Format("{0} {1} {2}", counter, item.first_name, item.last_name));

                context.Set<SDNItem>().Add(item);

                if (counter % savePerRecordCount == 0)
                {
                    Trace.WriteLine("Start save");
                    context.SaveChanges();
                    context.Dispose();

                    context = new BlackcheckEntities();
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    Trace.WriteLine("Finish save");
                }

                counter++;
            }

            context.SaveChanges();
            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;

            return context;
        }
        #endregion
    }
}
