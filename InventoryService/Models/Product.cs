using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace InventoryService.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
  
}
