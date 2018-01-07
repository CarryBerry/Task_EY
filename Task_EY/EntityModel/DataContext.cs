using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_EY.EntityModel
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }

        public DbSet<Content> Contents { get; set; }
    }
}
