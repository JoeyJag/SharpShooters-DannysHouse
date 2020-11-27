using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Sharpshooter.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }
        [StringLength(70)]
        public string Address { get; set; }
        [StringLength(40)]
        public string City { get; set; }
        
        
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        
        
        [StringLength(24)]
        public string Phone { get; set; }
        
        [DisplayName("Nickname")]     
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public bool PayAtStore { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


        public string ToString(Order order)
        {
            StringBuilder bob = new StringBuilder();

            bob.Append("<p>Order Information for Order: " + order.OrderId + "<br>Placed at: " + order.OrderDate + "</p>").AppendLine();
            bob.Append("<p>Name: " + order.FirstName + " " + order.LastName + "<br>");
            bob.Append("Address: " + order.Address + " " + order.City + " " + order.PostalCode + "<br>");
            bob.Append("Contact: " + order.Email + "     " + order.Phone + "</p>");

            bob.Append("<br>").AppendLine();
            bob.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            bob.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    bob.Append("<tr>");
                    output = "<td>" + item.MenuItem.MenuItemTitle + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.UnitPrice + "</td>";
                    bob.Append(output).AppendLine();
                    Console.WriteLine(output);
                    bob.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "No items ordered.";
            }
            bob.Append("</Table>");
            bob.Append("<b>");
            // Display footer 
            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", order.Total);
            bob.Append(footer).AppendLine();
            bob.Append("</b>");

            return bob.ToString();
        }
    }
}