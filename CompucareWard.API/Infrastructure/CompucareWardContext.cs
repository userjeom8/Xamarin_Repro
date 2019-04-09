using CompucareWard.API.Infrastructure.EntityConfigurations;
using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure
{
    public class CompucareWardContext : DbContext
    {
        public CompucareWardContext(DbContextOptions<CompucareWardContext> options) : base(options)
        {

        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertReason> AlertReasons { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<BedBooking> BedBookings { get; set; }
        public DbSet<Clinician> Clinicians { get; set; }
        public DbSet<CommonBooking> CommonBookings { get; set; }
        public DbSet<FormComponent> FormComponents { get; set; }
        public DbSet<FormResult> FormResults { get; set; }
        public DbSet<FormComponentResult> FormComponentResults { get; set; }
        public DbSet<FormSubcomponent> FormSubcomponents { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<InpatientBooking> InpatientBookings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AlertEntityTypeConfiguration());
            builder.ApplyConfiguration(new AlertReasonEntityTypeConfiguration());
            builder.ApplyConfiguration(new BedEntityTypeConfiguration());
            builder.ApplyConfiguration(new BedBookingEntityTypeConfiguration());
            builder.ApplyConfiguration(new ClinicianEntityTypeConfiguration());
            builder.ApplyConfiguration(new CommonBookingEntityTypeConfiguration());
            builder.ApplyConfiguration(new FormComponentEntityTypeConfiguration());
            builder.ApplyConfiguration(new FormComponentResultEntityTypeConfiguration());
            builder.ApplyConfiguration(new FormResultEntityTypeConfigurationConfiguration());
            builder.ApplyConfiguration(new FormSubcomponentEntityTypeConfiguration());
            builder.ApplyConfiguration(new GlobalSettingEntityTypeConfiguration());
            builder.ApplyConfiguration(new IngredientEntityTypeConfiguration());
            builder.ApplyConfiguration(new InpatientBookingEntityTypeConfiguration());
            builder.ApplyConfiguration(new LocationEntityTypeConfiguration());
            builder.ApplyConfiguration(new NextOfKinEntityTypeConfiguration());
            builder.ApplyConfiguration(new NurseEntityTypeConfiguration());
            builder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            builder.ApplyConfiguration(new PersonEntityTypeConfiguration());
            builder.ApplyConfiguration(new RelationshipEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());    
        }
    }

    public class CompucareWardContextDesignFactory : IDesignTimeDbContextFactory<CompucareWardContext>
    {
        public CompucareWardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CompucareWardContext>()
                .UseSqlServer("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.CatalogDb;Integrated Security=true");

            return new CompucareWardContext(optionsBuilder.Options);
        }
    }
}
