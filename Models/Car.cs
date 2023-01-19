using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CarMarket.Models
{
    public partial class Car
    {
        public Car()
        {
            CarImages = new HashSet<CarImages>();
        }

        public int CarId { get; set; }
        public int DeliveryId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string Weight { get; set; }
        public string OilType { get; set; }

        public virtual Delivery Delivery { get; set; }
        public virtual ICollection<CarImages> CarImages { get; set; }
    }
}
