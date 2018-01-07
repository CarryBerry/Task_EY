using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task_EY.EntityModel
{
    public class DBNumberManupulator
    {
        internal DataContext context;
        //internal DbSet<TEntity> dbSet;

        //public DBNumberManupulator(/*DataContext context*/)
        //{
        //    //this.context = context;
        //    this.dbSet = context.Set<TEntity>();
        //}

        

        public virtual IEnumerable<Content> Get(
            Expression<Func<Content, bool>> filter = null,
            Func<IQueryable<Content>, IOrderedQueryable<Content>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Content> query = context.Set<Content>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public long GetFirstColumnSum()
        {
            long sum = 0;
            
            using (DataContext context = new DataContext())
            {
                foreach (var data in context.Contents)
                {
                    sum += data.IntLine;
                }
            }

            return sum;
        }

        public double GetSecondColumnMedian()
        {
            IList<double> numbers = new List<double>();
            double median;

            using (DataContext context = new DataContext())
            {
                foreach (var data in context.Contents)
                {
                    numbers.Add(data.IntLine);
                }
            }

            if (numbers.Count() == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }

            int halfIndex = numbers.Count() / 2;
            var sortedNumbers = numbers.OrderBy(n => n);

            if ((numbers.Count() % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt((halfIndex - 1))) / 2);
            }

            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }

            return median;
        }
    }
}
