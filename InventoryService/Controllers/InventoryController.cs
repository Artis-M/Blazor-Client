using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
            var re = this.Request;
            var headers = re.Headers;
            try
            {
                string token = headers.GetCommaSeparatedValues("token").First();
                     Console.WriteLine(token + " token value");
                if (token.Equals("test"))
                {
                    Console.WriteLine("Success");
                    inventoryManager.AddProduct(product);
                    Console.WriteLine(product.ProductID);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Wack");
            }
          
        }
    }
}
