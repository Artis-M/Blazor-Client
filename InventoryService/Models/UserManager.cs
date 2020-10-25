using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using InventoryService.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace InventoryService.Models
{
    public class UserManager
    {
        private string usersFile = "Data/users.json";
        public List<User> users;
        public UserManager()
        {
            if (!File.Exists(usersFile) || File.ReadAllText(usersFile).Equals("null"))
            {
                users = new List<User>();
                User admin = new User
                {
                    Username = "admin",
                    Password = "admin",
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                };
                users.Add(admin);
                string json = JsonSerializer.Serialize(users);
                File.WriteAllText(usersFile, json);
            }
            else
            {
                string content = File.ReadAllText(usersFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }
        public void SaveUsers()
        {
            string usersAsJson = JsonSerializer.Serialize(users);
            File.WriteAllText(usersFile, usersAsJson);
        }
        public void GenerateToken(User user)
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            users.First(item => item == user).Token = token;
            SaveUsers();
        }
        public User checkUser(string username, string password)
        {
            User userBuffer = new User();
            try
            {
               userBuffer = users.First(user => user.Username.Equals(username));
            }
        catch(Exception e)
            {

            }
            if(userBuffer == null)
            {

            }

            if (!userBuffer.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }
            return userBuffer;
        }
        public bool isTokenValid(string token)
        {
            foreach(var item in users)
            {
                if (item.Token.Equals(token))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
