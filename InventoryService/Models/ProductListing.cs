using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class ProductListing
    {
        public Product product { get; set; }
        public int TotalQuantity {get; set;}
        public int Reserved { get; set; }
        public int AvailableQuantity { get; set; }
        public void CalculateAvailability()
        {
            AvailableQuantity = TotalQuantity - Reserved;
        }
    }
}
