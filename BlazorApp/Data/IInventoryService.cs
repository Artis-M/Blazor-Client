using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    interface IInventoryService
    {
        public Task<List<ProductListing>> getProductListings(string token);

        public List<Product> getProducts(string token);

        public Task addProduct(Product product, string token);

        public Task removeProductListing(ProductListing productListing, string token);

        public Task editProductListing(ProductListing productListing, string token);

        public Task editProductListingName(ProductListing productListing, string token);

        public Task editProductListingPrice(ProductListing productListing, string token);

    }
}
