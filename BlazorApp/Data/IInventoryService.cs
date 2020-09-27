using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    interface IInventoryService
    {
        public Task<List<ProductListing>> getProductListings();

        public List<Product> getProducts();

        public Task addProduct(Product product);

    }
}
