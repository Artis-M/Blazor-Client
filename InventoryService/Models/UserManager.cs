using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

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
                    Token = "test"
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
    }
}
