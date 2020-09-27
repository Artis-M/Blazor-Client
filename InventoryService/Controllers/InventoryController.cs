using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryService.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        InventoryManager inventoryManager = new InventoryManager();

        // GET api/inventory/listings
        [HttpGet("listings")]
        public string GetListings()
        {
            string rep = JsonSerializer.Serialize(inventoryManager.GetProductListings());
            return rep;
        }
        // GET api/inventory/products
        [HttpGet("products")]
        public string GetProducts()
        {
            string rep = JsonSerializer.Serialize(inventoryManager.GetProducts());
            return rep;
        }
        //To be finished
        // POST api/inventory
        [HttpPost("add/product")]
        public void PostProduct([FromBody] Product product)
        {
            inventoryManager.AddProduct(product);
        }
    }
}
