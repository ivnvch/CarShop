namespace CarWorkShop.Models.Entity
{
    public class Car
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string CarNumber { get; set; }
        public byte[]? Avatar { get; set; }
        //public int ProfileId { get; set; }
        //public Profile? Profile { get; set; }
        public int? RecordId { get; set; }
        public Record? Record { get; set; }
    }
}
