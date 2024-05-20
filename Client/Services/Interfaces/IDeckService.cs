using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Shared.Models;

namespace Memorize.Client.Services.Interfaces
{
    public interface IDeckService
    {
        public Task<IEnumerable<Card>?> GetCards(int id);
    }
}