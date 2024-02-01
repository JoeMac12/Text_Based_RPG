using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapClass
{
    internal class Map
    {
        private char[,] map;
        private int mapHeight;
        private int mapWidth;

        public Map(string filename)
        {
            LoadMap(filename);
        }

        public void LoadMap(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            mapHeight = lines.Length;
            mapWidth = lines[0].Length;
            map = new char[mapHeight, mapWidth];

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    map[y, x] = lines[y][x];
                }
            }
        }

        public void Display()
        {
            DrawBorder();

            for (int y = 0; y < mapHeight; y++)
            {
                Console.Write("|");
                for (int x = 0; x < mapWidth; x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.WriteLine("|");
            }

            DrawBorder();
        }

        public void DrawBorder()
        {
            Console.Write("+");
            for (int i = 0; i < mapWidth; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }
    }
}
