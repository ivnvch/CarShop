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

            modelBuilder.Entity<Car>(builder =>
            {
                builder.ToTable("Cars").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Record>(builder =>
            {
                builder.ToTable("Records").HasKey(x => x.Id);

                builder.HasOne(x => x.Car)
                .WithOne(c => c.Record)
                .HasForeignKey<Car>(c => c.RecordId).IsRequired();
            });
                        
        }
 
    }
}
