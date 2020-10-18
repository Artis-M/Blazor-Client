using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    interface IOrderService
    {
        public Task<List<Order>> GetOrders(string token);

        public Task CancelOrder(Order order, string token);

        public Task AcceptOrder(Order order, string token);

        public Task GenerateARandomOrder(string token);

    }
}
