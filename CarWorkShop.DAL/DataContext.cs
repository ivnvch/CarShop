using CarWorkShop.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarWorkShop.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Record> Records { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.FirstName).IsRequired();
                builder.Property(x => x.LastName).IsRequired();
                builder.Property(x => x.MiddleName).IsRequired();
                builder.Property(x => x.Age).IsRequired();

            });

            modelBuilder.Entity<Owner>(builder => 
            {
                builder.ToTable("Owners").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Login).IsRequired();
                builder.Property(x => x.Password).IsRequired();

                builder.HasOne(x => x.Profile)
                .WithOne(x => x.Owner)
                .HasPrincipalKey<Owner>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });
           

            modelBuilder.Entity<Profile>()
            .HasMany(p => p.Records)
             .WithOne(r => r.Profile)
              .HasForeignKey(r => r.ProfileId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Record>()
                .HasOne(r => r.Car)
                 .WithOne(c => c.Record)
                  .HasForeignKey<Car>(c => c.RecordId);


            
        }
 
    }
}
