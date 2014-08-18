using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShareMyDrive.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
    }
    public class CarContext : DbContext
    {
        public DbSet<CarModel> Cars { get; set; }
    }
}