using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharpshooter.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }

        [ForeignKey("Menu")]
        public int MenuID { get; set; }
        public virtual Menu Menu { get; set; }

        [ForeignKey("MenuGroup")]
        public int MenuGroupID { get; set; }
        public virtual MenuGroup MenuGroup { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<Review> Review { get; set; }



        [Required(ErrorMessage ="A title is required for the menu item")]
        [Display(Name ="Item Name")]
        public string MenuItemTitle { get; set; }

        [Display(Name ="Description")]
        public string MenuItemDescription { get; set; }

        [Display(Name = "Nutrition Information")]
        public string MenuItemNutrition { get; set; }

        [Display(Name = "Information about Ingredients")]
        public string MenuItemIngredients { get; set; }

        [Display(Name ="How many in serving")]
        public int MenuItemQuantity { get; set; }

        [Display(Name ="Food Image")]
        public string MenuItemImg { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }


        [Display(Name ="Cost Of Menu Item")]
        public decimal MenuItemCost { get; set; }
    }
}