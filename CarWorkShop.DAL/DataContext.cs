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
            modelBuilder.Entity<Owner>()
                .HasOne(o => o.Profile)
                 .WithOne(p => p.Owner)
                  .HasForeignKey<Profile>(p => p.OwnerId);

            modelBuilder.Entity<Profile>()
            .HasMany(p => p.Records)
             .WithOne(r => r.Profile)
              .HasForeignKey(r => r.ProfileId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Car)
                 .WithOne(c => c.Record)
                  .HasForeignKey<Car>(c => c.RecordId);


            //Create a user account
            Owner owner = new Owner()
            {
                Id = 3,
                Login = "lunasya",
                Password = "147258",
                Role = Models.Enum.Role.Owner,
                //ProfileId = 4

            };

            //create a car
            Car car = new Car()
            {
                Id = 1,
                Mark = "Nissan",
                Model = "GT-R",
                CarNumber = "7999 AI-7",
                //RecordId = 1
            };

            //To fill in a profile
            Profile profile = new Profile()
            {
                Id = 4,
                FirstName = "Авраам",
                LastName = "Линкольн",
                Age = 50,
                OwnerId = owner.Id,
            };

            owner.ProfileId = profile.Id;
            

            //create a Record
            Record record = new Record()
            {
                Id = 1,
                DateTime = DateTime.Now,
                Complaint = "Неисправность в выхлопной системе",
                CarId = car.Id, //?
                ProfileId = profile.Id
            };
            car.RecordId = record.Id;
            profile.Records.Add(record);

            modelBuilder.Entity<Owner>().HasData(owner);
            modelBuilder.Entity<Car>().HasData(car);
            modelBuilder.Entity<Profile>().HasData(profile);
            modelBuilder.Entity<Record>().HasData(record);

            //modelBuilder.Entity<Record>().HasData(
            //    new Record()
            //    {
            //        Id = 1,
            //        CarId = 1,
            //        DateTime = DateTime.Now,
            //        Complaint = "Всё найс",
            //        Profile = new Profile() { Id = 1, FirstName = "Дмитрий", MiddleName = "Иванович", LastName = "Тарасовец", Age = 21 }

            //    });
        }
 
    }
}
