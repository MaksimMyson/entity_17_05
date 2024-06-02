using Microsoft.EntityFrameworkCore;
using entity_17_05.Models;
using System.Collections.Generic;

namespace entity_17_05.Context
{
    public class GameLibraryContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("your_connection_string_here");
        }
    }
}