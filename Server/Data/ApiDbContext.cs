using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Memorize.Server.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}