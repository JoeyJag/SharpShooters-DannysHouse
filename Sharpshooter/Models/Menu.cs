using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharpshooter.Models
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }

        [Display(Name ="Menu Title")]
        public string MenuTitle { get; set; }

        [Display(Name ="Description")]
        public string MenuDescription { get; set; }

        public virtual List<MenuItem> MenuItems { get; set; }

        public virtual List<MenuGroup> MenuGroups { get; set; }
    }
}