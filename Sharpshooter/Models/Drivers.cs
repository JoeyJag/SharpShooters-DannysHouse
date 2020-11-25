using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sharpshooter.Models
{
    public class Drivers
    {
        [Key]
        public string DriverID { get; set; }

        [Display(Name ="First Name")]
        public string DriverName { get; set; }
        [Display(Name = "Surname")]
        public string DriverSurname { get; set; }
        [Display(Name ="Vehicle Type (Car or Bike):")]
        public string DriverVehicle { get; set; }
        [Display(Name ="Id Number:")]
        public string DriverIdNo { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}