using SouthFork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SouthFork.DAL
{
    public class SouthForkDBContext : DbContext
    {
        public SouthForkDBContext() : base("SouthForkDBContext") { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}