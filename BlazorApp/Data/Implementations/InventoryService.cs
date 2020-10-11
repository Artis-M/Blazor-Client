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
            List<ProductListing> listings = new List<ProductListing>();
            http.DefaultRequestHeaders.Add("token", token);
            string response = await http.GetStringAsync("api/inventory/listings");
            try
            {
                listings = JsonSerializer.Deserialize<List<ProductListing>>(response);
                listings = listings.OrderByDescending(item => item.product.ProductID).ToList();
                return listings;
            }
            catch (Exception e)
            {

            }
            return listings;
        }

        public List<Product> getProducts(string token)
        {
            throw new NotImplementedException();
        }
        public async Task addProduct(Product product, string token)
        {
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/inventory/add/product", product);
        }

        public async Task removeProductListing(ProductListing productListing, string token)
        {
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/inventory/remove/productListing", productListing);
        }

        public async Task editProductListing(ProductListing productListing, string token)
        {
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/inventory/edit/productListing", productListing);
        }

        public async Task editProductListingName(ProductListing productListing, string token)
        {
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/inventory/edit/productListingName", productListing);
        }

        public async Task editProductListingPrice(ProductListing productListing, string token)
        {
            http.DefaultRequestHeaders.Add("token", token);
            await http.PostAsJsonAsync("api/inventory/edit/productListingPrice", productListing);
        }
    }
}
