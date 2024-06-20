using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Shared.Models;


namespace Memorize.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> SignIn(User user);
        Task<bool> Logout();
        public Task<User?> GetCurrentUser();


    }
}