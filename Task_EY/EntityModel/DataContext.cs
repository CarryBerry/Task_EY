using System.Data.Entity;

namespace Task_EY.EntityModel
{
    public class DataContext : DbContext // using Code First.
    {
        public DataContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
        }

        public DbSet<Content> Contents { get; set; }
    }
}
