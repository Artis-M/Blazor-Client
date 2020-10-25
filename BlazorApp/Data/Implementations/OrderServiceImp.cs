using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Data.Implementations
{
    class OrderServiceImp : IOrderService
    {
        string uri = "https://localhost:5001/";
        public async Task AcceptOrder(Order order, string token)
        {
            HttpClient http = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/orders/accept", order);
        }

        public async Task CancelOrder(Order order, string token)
        {
            HttpClient http = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/orders/cancel", order);
        }
        public async Task GenerateARandomOrder(string token)
        {
            
            Product placeholder = new Product
            {
                Name = "a",
                Price = 1,
                ProductID = 0
            };
            HttpClient http = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            http.DefaultRequestHeaders.Add("token", token);
            HttpResponseMessage response = await http.PostAsJsonAsync("api/orders/generate", placeholder);
        }

        public async Task<List<Order>> GetOrders(string token)
        {
            HttpClient http = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            List<Order> orders = new List<Order>();
            http.DefaultRequestHeaders.Add("token", token);
            string response = await http.GetStringAsync("api/orders/all");
            try
            {
                orders = JsonSerializer.Deserialize<List<Order>>(response);
                orders = orders.OrderByDescending(item => item.OrderID).ToList();
                return orders;
            }
            catch (Exception e)
            {

            }
            return orders;
        }
    }
}
