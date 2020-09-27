using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace BlazorApp.Data
{
    public class InventoryService : IInventoryService
    {
        HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44306/")
        };

        public async Task<List<ProductListing>> getProductListings()
        {
            List<ProductListing> listings;
            listings = JsonSerializer.Deserialize<List<ProductListing>>(await http.GetStringAsync("api/inventory/listings"));
            listings.OrderByDescending(item => item.product.Name).ToList();
            return listings;
        }

        public List<Product> getProducts()
        {
            throw new NotImplementedException();
        }
        public async Task addProduct(Product product)
        {
            string json = JsonSerializer.Serialize(product);
            await http.PostAsJsonAsync("api/inventory/add/product", product);
        }
    }
}
