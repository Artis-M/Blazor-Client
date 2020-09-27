using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class OrderLine
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
