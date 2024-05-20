using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Shared.Models;

namespace Memorize.Client.Services.Interfaces
{
    public interface ILibraryService
    {
        public Task<List<Deck>?> GetCurrentUserDecks(User user);
    }
}