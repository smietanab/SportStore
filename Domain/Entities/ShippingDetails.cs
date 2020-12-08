using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
   public class ShippingDetails
    {
        [Required(ErrorMessage ="prosze podac")]
        public string Name { get; set; }

        [Required(ErrorMessage = "adres")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "wojewodztwo")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "kraj")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }

    }
}
