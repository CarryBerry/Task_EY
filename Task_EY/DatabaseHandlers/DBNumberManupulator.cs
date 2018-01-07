using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_EY.EntityModel
{
    public static class DBNumberManupulator
    {
        public static long GetFirstColumnSum()
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

        public static double GetSecondColumnMedian()
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
