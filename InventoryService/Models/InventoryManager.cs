using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
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
                orders = new List<Order>();
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
            string ordersAsJson = JsonSerializer.Serialize(orders);
            File.WriteAllText(orderFile, ordersAsJson);
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
            listing.CalculateAvailability();
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
            products.Remove(products.First(product => product.ProductID == productID));

            return true;
        }
        public bool removeProductListing(ProductListing productListing)
        {
            try
            {
                productListings.Remove(productListings.First(item => item.product.ProductID == productListing.product.ProductID));
                WriteProductListingsToFile();
                removeProduct(productListing.product.ProductID);
                Console.WriteLine("Listing Deleted");
                return true;
            }
            catch (Exception e)
            {

            }
            return false;

        }
        public void editProductListing(ProductListing productListing)
        {
            try
            {
                productListings.Remove(productListings.First(item => item.product.ProductID == productListing.product.ProductID));
                productListings.Add(productListing);
                WriteProductListingsToFile();
            }
            catch (Exception e)
            {

            }
        }
        public void editProductListingName(ProductListing productListing)
        {
            try
            {
                productListings.Remove(productListings.First(item => item.product.ProductID == productListing.product.ProductID));
                productListings.Add(productListing);
                Product toEditProduct = products.First(item => item.ProductID == productListing.product.ProductID);
                products.Remove(toEditProduct);
                toEditProduct.Name = productListing.product.Name;
                products.Add(toEditProduct);
                WriteProductsToFile();
                WriteProductListingsToFile();
            }
            catch (Exception e)
            {

            }
        }
        public void editProductListingPrice(ProductListing productListing)
        {
            try
            {
                productListings.Remove(productListings.First(item => item.product.ProductID == productListing.product.ProductID));
                productListings.Add(productListing);
                Product toEditProduct = products.First(item => item.ProductID == productListing.product.ProductID);
                products.Remove(toEditProduct);
                toEditProduct.Price = productListing.product.Price;
                products.Add(toEditProduct);
                WriteProductsToFile();
                WriteProductListingsToFile();
            }
            catch (Exception e)
            {

            }
        }
        public void GenerateOrder()
        {
            Random random = new Random();
            int randomProdcutInt = random.Next(products.Count);
            Console.WriteLine(randomProdcutInt);
            int randomAmount = random.Next(100);
            int OrderIdNew;

            try
            {
                OrderIdNew = orders.Max(order => order.OrderID);
            }
            catch (Exception e)
            {
                OrderIdNew = 1;
            }

            OrderIdNew++;
            OrderLine orderLine = new OrderLine
            {
                product = products[randomProdcutInt],
                Quantity = randomAmount
            };
            randomProdcutInt = random.Next(products.Count);
            randomAmount = random.Next(100);
            OrderLine orderLine2 = new OrderLine
            {
                product = products[randomProdcutInt],
                Quantity = randomAmount
            };
            List<OrderLine> orderLines = new List<OrderLine>();
            orderLines.Add(orderLine);
            orderLines.Add(orderLine2);
            Order newOrder = new Order
            {
                OrderID = OrderIdNew,
                orderLine = orderLines,
            };
            newOrder.CalculatePrice();
            orders.Add(newOrder);
            ProductListing toEditPL1 = productListings.First(productLi => orderLine.product.ProductID == productLi.product.ProductID);
            ProductListing toEditPL2 = productListings.First(productLi => orderLine2.product.ProductID == productLi.product.ProductID);

            productListings.Remove(productListings.First(productLi => orderLine.product.ProductID == productLi.product.ProductID));
            productListings.Remove(productListings.First(productLi => orderLine2.product.ProductID == productLi.product.ProductID));

            toEditPL1.Reserved += orderLine.Quantity;
            toEditPL2.Reserved += orderLine2.Quantity;
            toEditPL1.CalculateAvailability();
            toEditPL2.CalculateAvailability();
            productListings.Add(toEditPL1);
            productListings.Add(toEditPL2);
            WriteProductListingsToFile();
            WriteOrdersToFile();
        }
        public void AcceptOrder(Order order)
        {
            if (!order.IsCancelled && !order.IsAccepted)
            {
                order.IsAccepted = true;
                orders.Remove(orders.First(orderItem => orderItem.OrderID == order.OrderID));
                orders.Add(order);
                WriteOrdersToFile();
                foreach (var item in order.orderLine)
                {
                    Product prod = item.product;
                    ProductListing toEdit = productListings.First(itemPD => itemPD.product.ProductID == prod.ProductID);
                    productListings.Remove(toEdit);
                    toEdit.Reserved -= item.Quantity;
                    toEdit.TotalQuantity -= item.Quantity;
                    productListings.Add(toEdit);
                    WriteProductListingsToFile();
                }
            }
        }
        public void CancelOrder(Order order)
        {
            if (!order.IsCancelled && !order.IsAccepted)
            {
                order.IsCancelled = true;
                orders.Remove(orders.First(orderItem => orderItem.OrderID == order.OrderID));
                orders.Add(order);
                foreach (var item in order.orderLine)
                {
                    Product prod = item.product;
                    ProductListing toEdit = productListings.First(itemPD => itemPD.product.ProductID == prod.ProductID);
                    productListings.Remove(toEdit);
                    toEdit.Reserved -= item.Quantity;
                    productListings.Add(toEdit);
                    WriteProductListingsToFile();
                }
                WriteOrdersToFile();
            }
        }
        //Returns various lists.
        public List<ProductListing> GetProductListings()
        {
            foreach (var item in productListings)
            {
                item.CalculateAvailability();
            }
            WriteProductListingsToFile();
            return productListings;
        }
        public List<Product> GetProducts()
        {
            return products;
        }
        public List<Order> GetOrders()
        {
            return orders;
        }
    }
}
