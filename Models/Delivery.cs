using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CarMarket.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Car = new HashSet<Car>();
        }

        public int DeliveryId { get; set; }
        public string Provider { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
