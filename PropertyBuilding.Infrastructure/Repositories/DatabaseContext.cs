using Microsoft.EntityFrameworkCore;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyBuilding.Infrastructure.Repositories
{
    public class DatabaseContext : DbContext 
    {
        public DbSet<Owner> owner { get; set; }
        public DbSet<Property> property { get; set; }
        public DbSet<PropertyImage> propertyImage { get; set; }
        public DbSet<PropertyTrace> propertyTrace { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
