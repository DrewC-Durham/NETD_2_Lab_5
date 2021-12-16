using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Make sure you use the Nu Get package manager to instal the Microsoft Entity Framework Core
using Microsoft.EntityFrameworkCore;

namespace Lab_5_Final.Models
{
    public class FootballContext : DbContext
    {
        public FootballContext(DbContextOptions<FootballContext> options) : base(options)
        {



        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Superbowl> Superbowls { get; set; }
    }
}
