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
        [Range(0, 5, ErrorMessage = "Rating can be from 0 - 5")]
        public int Rating { get; set; }
        public virtual MenuItem MenuItem { get; set; }



    
     
    }

   

}