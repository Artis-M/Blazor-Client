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
            var re = this.Request;
            var headers = re.Headers;
            try
            {
                string token = headers.GetCommaSeparatedValues("token").First();
                Console.WriteLine(token + " token value");
                if (token.Equals("test"))
                {
                    string rep = JsonSerializer.Serialize(inventoryManager.GetProductListings());
                    return rep;
                }
                return "Invalid token: " + token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Invalid Token, is it blank?"); 
            }
            return "Invalid token";
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
                Console.WriteLine("Invalid Token");
            }
          
        }
        [HttpPost("remove/productListing")]
        public void RemoveProductListing([FromBody] ProductListing productListing)
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
                    inventoryManager.removeProductListing(productListing);
                    Console.WriteLine($"{productListing.product.Name} to be removed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Token");
            }

        }
        [HttpPost("edit/productListing")]
        public void EditProductListing([FromBody] ProductListing productListing)
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
                    inventoryManager.editProductListing(productListing);
                    Console.WriteLine($"{productListing.product.Name} to be edited");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Token");
            }

        }
        [HttpPost("edit/productListingName")]
        public void EditProductListingName([FromBody] ProductListing productListing)
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
                    inventoryManager.editProductListingName(productListing);
                    Console.WriteLine($"{productListing.product.Name} to be edited");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Token");
            }

        }
        [HttpPost("edit/productListingPrice")]
        public void EditProductListingPrice([FromBody] ProductListing productListing)
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
                    inventoryManager.editProductListingPrice(productListing);
                    Console.WriteLine($"{productListing.product.Name} to be edited");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Token");
            }

        }
    }
}
