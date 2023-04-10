﻿using CarWorkShop.Models.Enum;

namespace CarWorkShop.Models.Entity
{
    public class Owner
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int ? ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }
}
