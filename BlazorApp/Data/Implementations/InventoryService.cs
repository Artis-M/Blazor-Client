using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace BlazorApp.Data
{
    public class InventoryService : IInventoryService
    {
        HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/")
        };
        public async Task<List<ProductListing>> getProductListings(string token)
        {
            List<ProductListing> listings;
            http.DefaultRequestHeaders.Add("token", token);
            listings = JsonSerializer.Deserialize<List<ProductListing>>(await http.GetStringAsync("api/inventory/listings"));
            listings.OrderByDescending(item => item.product.Name).ToList();
            return listings;
        }

        public List<Product> getProducts(string token)
        {
            throw new NotImplementedException();
        }
        public async Task addProduct(Product product, String token)
        {
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/inventory/add/product", product);
        }
    }
}
