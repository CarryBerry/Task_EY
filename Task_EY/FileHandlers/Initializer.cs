

namespace Task_EY
{
    public static class Initializer
    {
        private static LineGenerator generator;
        
        public static LineGenerator Initialize() //helps to generate random lines
        {
                if (generator == null)
                {
                    generator = new LineGenerator();
                }

                return generator;
        }
    }
}
