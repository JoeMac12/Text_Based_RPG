using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map;

namespace Text_Based_RPG__First_Playable
{
    internal class Program
    {
        static void Main(string[] args) // Main call
        {
            MapLoader Map = new MapLoader("mapArea.txt");
            Console.ReadKey(true);
        }
    }
}