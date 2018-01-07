using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task_EY
{
    public class LineGenerator
    {
        //private string dateLine;
        //private string latinLine;
        //private string cyrillicLine;
        //private int intLine;
        //private double doubleLine;

        //private Random random = new Random();

        private string latinChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string cyrillicChars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private int maxInt = 100000000, minDouble = 1, maxDouble = 20;

        public string DateLine { get; private set; }
        public string LatinLine { get; private set; }
        public string CyrillicLine { get; private set; }
        public int IntLine { get; private set; }
        public double DoubleLine { get; private set; }

        //public LineGenerator ()
        //{
        //    DateLine = dateLine;
        //    LatinLine = latinLine;
        //    CyrillicLine = cyrillicLine;
        //    IntLine = intLine;
        //    DoubleLine = doubleLine;
        //}

        //Random random = new Random((int)DateTime.Now.Ticks);
        Random random = new Random();

        private char[] GetRandomLine(string chars, int length)
        {
            return (Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]))
                .ToArray();
        }

        private DateTime GetRandomDate()
        {
            return DateTime.Now.AddYears(-5).AddDays(random.Next((DateTime.Now - DateTime.Now.AddYears(-5)).Days));
        }

        //public int GetRandomInt()
        //{
        //    return (2 * random.Next(maxInt / 2));
        //}

        public string GenerateLine()
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
