using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sharpshooter.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public bool OrderStatus { get; set; }
        public bool DeliveryStatus { get; set; }
        public bool DeliveryProcess { get; set; }
        public string CurrentDeliveryProcess { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Order Order { get; set; }



        public string getDeliveryProcess()
        {

            
            if(DeliveryProcess == true)
            {
                CurrentDeliveryProcess = "In Progress";
            }
            else
            {
                CurrentDeliveryProcess = "Open";
            }

            return CurrentDeliveryProcess;
        }
    }
}