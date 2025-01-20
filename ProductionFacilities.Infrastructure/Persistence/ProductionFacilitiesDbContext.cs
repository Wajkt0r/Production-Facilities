using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductionFacilities.Domain.Entities;

namespace ProductionFacilities.Infrastructure.Persistence
{
    public class ProductionFacilitiesDbContext : DbContext
    {
        public DbSet<ProductionFacility> ProductionFacilities { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Domain.Entities.Contract> Contracts {  get; set; } 

        public ProductionFacilitiesDbContext(DbContextOptions<ProductionFacilitiesDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSeeding((context, _) =>
                {
                    if (!context.Set<ProductionFacility>().Any())
                    {
                        context.Set<ProductionFacility>()
                            .AddRange(new ProductionFacility() { Code = "FAC001", Name = "Main Production Facility", StandardAreaForEquipment = 150.0 }, 
                                 new ProductionFacility() { Code = "FAC002", Name = "Secondary Facility", StandardAreaForEquipment = 100.0 });                        
                        context.SaveChanges();
                    }
                    if (!context.Set<EquipmentType>().Any())
                    {
                        context.Set<EquipmentType>()
                            .AddRange(new EquipmentType() { Code = "EQ001", Name = "Drill Machine", Area = 25.0 },
                                 new EquipmentType() { Code = "EQ002", Name = "Lathe Machine", Area = 30.0 },
                                 new EquipmentType() { Code = "EQ003", Name = "CNC Machine", Area = 40.0 });
                        context.SaveChanges();
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    if (!await context.Set<ProductionFacility>().AnyAsync(cancellationToken))
                    {
                        context.Set<ProductionFacility>()
                            .AddRange(new ProductionFacility() { Code = "FAC001", Name = "Main Production Facility", StandardAreaForEquipment = 150.0 },
                                 new ProductionFacility() { Code = "FAC002", Name = "Secondary Facility", StandardAreaForEquipment = 100.0 });
                        await context.SaveChangesAsync(cancellationToken);
                    }
                    if (!await context.Set<EquipmentType>().AnyAsync(cancellationToken))
                    {
                        context.Set<EquipmentType>()
                            .AddRange(new EquipmentType() { Code = "EQ001", Name = "Drill Machine", Area = 25.0 },
                                 new EquipmentType() { Code = "EQ002", Name = "Lathe Machine", Area = 30.0 },
                                 new EquipmentType() { Code = "EQ003", Name = "CNC Machine", Area = 40.0 });
                        await context.SaveChangesAsync(cancellationToken);
                    }
                });




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductionFacility>()
                .HasKey(f => f.Code);

            modelBuilder.Entity<EquipmentType>()
                .HasKey(t => t.Code);

            modelBuilder.Entity<Domain.Entities.Contract>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.ProductionFacilityCode)
                    .IsRequired();
                entity.Property(c => c.EquipmentTypeCode)
                    .IsRequired();

                entity.HasOne(c => c.ProductionFacility)
                    .WithMany()
                    .HasForeignKey(c => c.ProductionFacilityCode)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.EquipmentType)
                    .WithMany()
                    .HasForeignKey(c => c.EquipmentTypeCode)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
