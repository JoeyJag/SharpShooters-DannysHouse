using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharpshooter.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [ForeignKey("MenuItem")]
        public int MenuItemID { get; set; }
        public string UserName { get; set; }
        public string ReviewOfItem{ get; set; }
        public int Rating { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public string StarRating { get; set; }


        public int GetRatingNumb()
        {
            if (StarRating == "OneStar")
            {
                Rating = 1;
            }
            else if (StarRating == "TwoStar")
            {
                Rating = 2;
            }
            else if (StarRating == "ThreeStar")
            {
                Rating = 3;
            }
            else if (StarRating == "FourStar")
            {
                Rating = 4;
            }
            else if (StarRating == "FiveStar")
            {
                Rating = 5;
            }
            else
            {
                Rating = 0;
            }

            return Rating;
        }
    }

   

}