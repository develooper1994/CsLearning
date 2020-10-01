using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace EntityFrameworkDemo
{
    public class EtradeContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
