using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace InventoryService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager userManager = new UserManager();

        [HttpGet("token")]
        public string Get()
        {
            var re = this.Request;
            var headers = re.Headers;
            string username = "";
            string password = "";
            try
            {
                username = headers.GetCommaSeparatedValues("username").First();
                password = headers.GetCommaSeparatedValues("username").First();
            }
            catch (Exception e)
            {

            }
            User buffer = userManager.checkUser(username, password);
            clientUser userToSend = new clientUser
            {
                Username = buffer.Username,
            Password = buffer.Password
            };
          
            string json = JsonSerializer.Serialize(userToSend);
            return json;
        }
        struct clientUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
    }
