using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class Order
    {
        public List<OrderLine> orderLine { get; set; }
        public int OrderID { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsCancelled { get; set; }

        public double OrderTotal { get; set; }

        public void CalculatePrice()
        {
            foreach (var orderItem in orderLine)
            {
                OrderTotal += orderItem.product.Price;
            }
        }
    }
}
