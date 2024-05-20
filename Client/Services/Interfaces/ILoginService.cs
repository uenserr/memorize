using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Shared.Models;


namespace Memorize.Client.Services.Interfaces
{
    public interface ILoginService
    {
        public static User CurrentUser { get; set; }
        Task<bool> Login(User user);
        Task<bool> Logout();
        public Task<User?> GetCurrentUser();


    }
}