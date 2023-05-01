namespace CarWorkShop.Models.Entity
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string? Complaint { get; set; } //жалобы 

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public Car Car { get; set; }
    }
}
