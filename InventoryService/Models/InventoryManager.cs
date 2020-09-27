using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class InventoryManager
    {
        private string productFile = "Data/products.json";
        private string orderFile = "Data/orders.json";
        private string productListingFile = "Data/product_listing.json";
        private List<ProductListing> productListings;
        private List<Product> products;
        private List<Order> orders;
        public InventoryManager()
        {
            if (!File.Exists(orderFile) || File.ReadAllText(orderFile).Equals("null"))
            {
                WriteOrdersToFile();
            }
            else
            {
                string content = File.ReadAllText(orderFile);
                orders = JsonSerializer.Deserialize<List<Order>>(content);
            }
            if (!File.Exists(productFile) || File.ReadAllText(productFile).Equals("null"))
            {
                SeedProducts();
            }
            else
            {
                string content = File.ReadAllText(productFile);
                products = JsonSerializer.Deserialize<List<Product>>(content);
            }
            if (!File.Exists(productListingFile) || File.ReadAllText(productListingFile).Equals("null"))
            {
                SeedProductListings();
            }
            else
            {
                string content = File.ReadAllText(productListingFile);
                productListings = JsonSerializer.Deserialize<List<ProductListing>>(content);
            }
        }
        //Adds products to the list in case there are none added.
        public void SeedProducts()
        {
            Product[] productsList =
             {
                new Product
                {
                    ProductID = 321,
                    Name = "Cherry MX Red Switches 10 pack",
                    Price = 6
                },
                new Product
                {
                           ProductID = 322,
                    Name = "Cherry MX Red Switches 100 pack",
                    Price = 50
                },
                        new Product
                {
                    ProductID = 319,
                    Name = "Gateron Black Ink Switches 100 pack",
                    Price = 80
                }
            };
            products = productsList.ToList();
            WriteProductsToFile();
        }

        private void SeedProductListings()
        {
            ProductListing[] listings = new ProductListing[products.Count];
            Random rng = new Random();
            int i = 0;
            foreach (var product in products)
            {
                ProductListing listing = new ProductListing
                {
                    product = product,
                    Reserved = 0,
                    TotalQuantity = rng.Next(1, 200)
                };
                listings[i] = listing;
                i++;
            }
            productListings = listings.ToList();
            WriteProductListingsToFile();
        }

        //Saves the lists to a JSON file, to avoid having to use a proper database.
        public void WriteOrdersToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(orders);
            File.WriteAllText(orderFile, productsAsJson);
        }
        public void WriteProductsToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(products);
            File.WriteAllText(productFile, productsAsJson);
        }
        private void WriteProductListingsToFile()
        {

            string productsAsJson = JsonSerializer.Serialize(productListings);
            File.WriteAllText(productListingFile, productsAsJson);
        }

   
        //Adds a new product and then adds it to a listing
        public void AddProduct(Product product)
        {
            int id = products.Max(product => product.ProductID);

            Product pd = new Product
            {
                Name = product.Name,
                Price = product.Price,
                ProductID = ++id
            };
            products.Add(pd);
            AddProductToListing(pd);
            WriteProductsToFile();
        }
        public void AddProductToListing(Product product)
        {
            ProductListing listing = new ProductListing
            {
                product = product,
                TotalQuantity = 0,
                Reserved = 0
            };
            productListings.Add(listing);
            WriteProductListingsToFile();
        }
        //Removes a product.
        public bool removeProduct(int productID)
        {
            foreach (var item in productListings)
            {
                if (item.product.ProductID == productID)
                {
                    Console.WriteLine($"Unable to remove product since it is in use");
                    return false;
                }
            }
            products.First(product => product.ProductID == productID);
            return true;
        }
        //Returns various lists.
        public List<ProductListing> GetProductListings()
        {
            return productListings;
        }
        public List<Product> GetProducts()
        {
            return products;
        }
    }
}
