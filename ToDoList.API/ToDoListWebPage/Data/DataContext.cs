using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebPage.Models;

namespace ToDoListWebPage.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Done> Done { get; set; }
        public DbSet<inProgress> inProgress { get; set; }
        public DbSet<Pending> Pendings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

