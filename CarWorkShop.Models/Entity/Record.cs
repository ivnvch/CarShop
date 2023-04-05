namespace CarWorkShop.Models.Entity
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string? Complaint { get; set; } //жалобы 

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
