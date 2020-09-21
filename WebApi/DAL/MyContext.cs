using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DAL
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Country> countrty { get; set; }
        public DbSet<State> state { get; set; }
        public DbSet<City> city { get; set; }
        public DbSet<PostalCode> PostalCode { get; set; }
        public DbSet<Street> street { get; set; }
        public DbSet<House> house { get; set; }

       


    }
}
