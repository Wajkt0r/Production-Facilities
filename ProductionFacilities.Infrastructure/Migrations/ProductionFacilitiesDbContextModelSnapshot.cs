﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductionFacilities.Infrastructure.Persistence;

#nullable disable

namespace ProductionFacilities.Infrastructure.Migrations
{
    [DbContext(typeof(ProductionFacilitiesDbContext))]
    partial class ProductionFacilitiesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductionFacilities.Domain.Entities.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EquipmentTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductionFacilityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UnitsNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeCode");

                    b.HasIndex("ProductionFacilityCode");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ProductionFacilities.Domain.Entities.EquipmentType", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("EquipmentTypes");
                });

            modelBuilder.Entity("ProductionFacilities.Domain.Entities.ProductionFacility", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StandardAreaForEquipment")
                        .HasColumnType("float");

                    b.HasKey("Code");

                    b.ToTable("ProductionFacilities");
                });

            modelBuilder.Entity("ProductionFacilities.Domain.Entities.Contract", b =>
                {
                    b.HasOne("ProductionFacilities.Domain.Entities.EquipmentType", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("EquipmentTypeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductionFacilities.Domain.Entities.ProductionFacility", "ProductionFacility")
                        .WithMany()
                        .HasForeignKey("ProductionFacilityCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentType");

                    b.Navigation("ProductionFacility");
                });
#pragma warning restore 612, 618
        }
    }
}
