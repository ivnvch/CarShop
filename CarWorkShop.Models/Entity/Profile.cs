﻿namespace CarWorkShop.Models.Entity
{
    public class Profile
    {

        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public short Age { get; set; }
        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
