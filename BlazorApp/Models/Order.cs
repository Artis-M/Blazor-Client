using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public class Order
    {
        public List<OrderLine> orderLine { get; set; }
        public int OrderID { get; set; }

        public double OrderTotal;

        public Order()
        {
            orderLine.ForEach(delegate (OrderLine orderLine)
            {
                OrderTotal += orderLine.product.Price;
            });
        }
    }
}
