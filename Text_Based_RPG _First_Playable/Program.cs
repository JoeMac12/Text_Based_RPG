using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable
{
    internal class Program
    {
        static void Main(string[] args) // Main call
        {
            Map mapArea = new Map("mapArea.txt");
            Console.ReadKey(true);

            mapArea.DisplayMap();
        }
    }
}
