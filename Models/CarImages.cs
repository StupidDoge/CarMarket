using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CarMarket.Models
{
    public partial class CarImages
    {
        public int ImageId { get; set; }
        public int CarId { get; set; }
        public string Image { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile File { get; set; }

        public virtual Car ImageNavigation { get; set; }
    }
}
