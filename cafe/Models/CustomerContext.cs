using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cafe.Models
{
    public class CustomerContext: DbContext
    {

        public CustomerContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }
    }
}
