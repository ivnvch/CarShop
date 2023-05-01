using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Helpers;
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

                builder.HasData(new Profile
                {
                    Id = 1,
                    OwnerId = 1
                });
            });

            modelBuilder.Entity<Owner>(builder => 
            {
                builder.ToTable("Owners").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Login).IsRequired();
                builder.Property(x => x.Password).IsRequired();

                builder.HasOne(o => o.Profile)
                .WithOne(p => p.Owner)
                .HasForeignKey<Profile>(x => x.OwnerId).IsRequired();

                builder.HasData(new Owner
                {
                    Id = 1,
                    Login = "dima@mail.ru",
                    Password = HashPasswordHelpers.HashPassword("123456"),
                    Role = Models.Enum.Role.Admin
                });
            });

            modelBuilder.Entity<Car>(builder =>
            {
                builder.ToTable("Cars").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.HasData(new Car
                {
                    Id = 1,
                    CarNumber = "3109 AE-3",
                    Mark = "B3",
                    Model = "Audi",
                    RecordId = 1,

                });
            });

            modelBuilder.Entity<Record>(builder =>
            {
                builder.ToTable("Records").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.HasOne(r => r.Car)
                .WithOne(c => c.Record)
                .HasForeignKey<Car>(c => c.RecordId).IsRequired(); //.HasForeignKey<Record>(c => c.Id).IsRequired();

                builder.HasOne(r => r.Owner)
                .WithMany(o => o.Records)
                .HasForeignKey(r => r.OwnerId);

                builder.HasData(new Record
                {
                    Id = 1,
                    Complaint = "Проблемы в системе зажигания",
                    DateTime = DateTime.Now,
                    OwnerId = 1,
                });
            });
                        
        }
 
    }
}
