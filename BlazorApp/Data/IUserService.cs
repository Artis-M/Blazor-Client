using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public interface IUserService
    {
      Task<User> Validate(string username, string password);
    }
}
