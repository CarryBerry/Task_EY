using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_EY
{
    public static class Initializer
    {
        //    private static Random random;
        //    private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static LineGenerator generator;

        //private static void Init()
        //{
        //    if (random == null) random = new Random();
        //}

        public static LineGenerator Initialize()
        {
                if (generator == null)
                {
                    generator = new LineGenerator();
                }
                return generator;
        }

        //    public static int Random(int min, int max)
        //    {
        //        Init();
        //        return random.Next(min, max);
        //    }

        //    public static int Random(int max)
        //    {
        //        Init();
        //        return random.Next(max);
        //    }

        //    public static double Random()
        //    {
        //        Init();
        //        return random.NextDouble();
        //    }
        //}




        //public static class StaticRandom
        //{
        //    private static int seed;

        //    private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        //        (() => new Random(Interlocked.Increment(ref seed)));

        //    static StaticRandom()
        //    {
        //        seed = Environment.TickCount;
        //    }

        //    public static Random Instance { get { return threadLocal.Value; } }
    }
}
