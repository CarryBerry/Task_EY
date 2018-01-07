using System;
using System.Linq;

namespace Task_EY
{
    public class LineGenerator
    {
        private string latinChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string cyrillicChars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private int maxInt = 100000000, minDouble = 1, maxDouble = 20;

        public string DateLine { get; private set; }
        public string LatinLine { get; private set; }
        public string CyrillicLine { get; private set; }
        public int IntLine { get; private set; }
        public double DoubleLine { get; private set; }
        
        Random random = new Random();

        private char[] GetRandomLine(string chars, int length) // generates random line
        {
            return (Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]))
                .ToArray();
        }

        private DateTime GetRandomDate() // generates random date from last 5 years
        {
            return DateTime.Now.AddYears(-5).AddDays(random.Next((DateTime.Now - DateTime.Now.AddYears(-5)).Days));
        }
        
        public string GenerateLine() // generate objects and concatenate to line
        {
            DateLine = GetRandomDate().ToString("d");
            LatinLine = new string(GetRandomLine(latinChars, 10));
            CyrillicLine = new string(GetRandomLine(cyrillicChars, 10));
            IntLine = (2 * random.Next(maxInt / 2));
            DoubleLine = Math.Round(random.NextDouble() * random.Next(minDouble, maxDouble-minDouble) + minDouble, 8);
            return DateLine + " " + LatinLine + " " + CyrillicLine + " " + IntLine + " " + DoubleLine;
        }
    }
}
