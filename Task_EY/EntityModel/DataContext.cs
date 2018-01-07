using System.Data.Entity;

namespace Task_EY.EntityModel
{
    public class DataContext : DbContext // using Code First.
    {
        public DataContext() : base() { } // DB name would be Task_EY.EntityModel.DataContext

        public DbSet<Content> Contents { get; set; }
    }
}
