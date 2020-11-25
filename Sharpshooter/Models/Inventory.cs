using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sharpshooter.Models
{
    public enum InventoryItemType
    {
        Meats=1,
       Drinks=2,
        Vegetables=3,
        Sauces=4,
        Bread=5,
        Other=6,
        Dairy=7
    }
    public class Inventory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public InventoryItemType Type { get; set; }
        public int QuantityRemaining { get; set; }
        public decimal PricePerUnit { get; set; }

    }
}