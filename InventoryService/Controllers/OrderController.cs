using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace InventoryService.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        InventoryManager inventoryManager = new InventoryManager();
        UserManager userManager = new UserManager();

        [HttpGet("all")]
        public string Get()
        {
            var re = this.Request;
            var headers = re.Headers;
            try
            {
                string token = headers.GetCommaSeparatedValues("token").First();
                Console.WriteLine(" token value: " + token);
                if (userManager.isTokenValid(token))
                {
                    string rep = JsonSerializer.Serialize(inventoryManager.GetOrders());
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
        [HttpPost("cancel")]
        public string CancelOrder([FromBody] Order order)
        {
            var re = this.Request;
            var headers = re.Headers;
            try
            {
                string token = headers.GetCommaSeparatedValues("token").First();
                Console.WriteLine(" token value: " + token);
                if (userManager.isTokenValid(token))
                {
                    inventoryManager.CancelOrder(order);

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
        [HttpPost("accept")]
        public string AcceptOrder([FromBody] Order order)
        {
            var re = this.Request;
            var headers = re.Headers;
            try
            {
                string token = headers.GetCommaSeparatedValues("token").First();
                Console.WriteLine(" token value: " + token);
                if (userManager.isTokenValid(token))
                {
                    inventoryManager.AcceptOrder(order);
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

        [HttpPost("generate")]
        public string CreateRandomOrder()
        {
            var re = this.Request;
            var headers = re.Headers;
            try
            {
                string token = headers.GetCommaSeparatedValues("token").First();
                Console.WriteLine(" token value: " + token);
                if (userManager.isTokenValid(token))
                {
                    inventoryManager.GenerateOrder();
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
    }
}
