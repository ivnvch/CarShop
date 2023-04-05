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
            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 1,
                    Mark = "Audi",
                    Model = "R8",
                    CarNumber = "3109-АЕ-3"
                });

            modelBuilder.Entity<Record>().HasData(
                new Record()
                {
                    Id = 1,
                    CarId = 1,
                    DateTime = DateTime.Now,
                    Complaint = "Всё найс",
                    Profile = new Profile() { Id = 1, FirstName = "Дмитрий", MiddleName = "Иванович", LastName = "Тарасовец", Age = 21 }

                });
        }
    }
}
