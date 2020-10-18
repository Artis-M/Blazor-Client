using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public class ProductListing
    {
        public Product product { get; set; }
        public int TotalQuantity { get; set; }
        public int Reserved { get; set; }
        public int AvailableQuantity { get; set;}
    }
}
