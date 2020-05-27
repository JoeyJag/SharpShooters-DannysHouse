using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sharpshooter.Models;
using System.ComponentModel.DataAnnotations;

namespace Sharpshooter.ViewModel
{
    public class ShoppingCartViewModel
    {
        [Key]
        public int ID { get; set; }
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}