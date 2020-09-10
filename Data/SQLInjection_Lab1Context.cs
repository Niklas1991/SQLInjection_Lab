using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLInjection_Lab1.Models;

namespace SQLInjection_Lab1.Data
{
    public class SQLInjection_Lab1Context : DbContext
    {
        public SQLInjection_Lab1Context (DbContextOptions<SQLInjection_Lab1Context> options)
            : base(options)
        {
        }


        public DbSet<SQLInjection_Lab1.Models.Products> Products { get; set; }
    }
}
