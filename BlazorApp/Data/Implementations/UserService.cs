using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class UserService : IUserService
    {
        HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/users/")
        };
        public async Task<User> Validate(string username, string password)
        {
            http.DefaultRequestHeaders.Add("username", username);
            http.DefaultRequestHeaders.Add("password", password);
            // User user = JsonSerializer.Deserialize<User>(await http.GetStringAsync("token"));
            //placeholder for testing
            User user = new User
            {
                Username = "admin",
                Token = "test"
            };
            Console.WriteLine("Getting login");
            if (user == null)
            {
                throw new Exception("Incorrect credentials");
            }
            return user;
        }
    }
}
