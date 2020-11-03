using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharpshooter.Models
{
    public class RestaurantReview
    {
        [Key]
        public int RestaurantReviewID { get; set; }
        public string UserName { get; set; }
        public string ReviewOfRestraurant { get; set; }
        [Range(0, 5, ErrorMessage = "Rating can be from 0 - 5")]
        public int Rating { get; set; }
    }
}