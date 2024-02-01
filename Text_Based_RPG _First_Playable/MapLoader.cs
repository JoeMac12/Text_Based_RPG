using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG_First_Playable
{
    internal class MapLoader
    {
        private char[,] map;
        private int mapWidth;
        private int mapHeight;

        public MapLoader()
        {
        }

        public void LoadMap(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            mapHeight = lines.Length;
            mapWidth = lines[0].Length;
            map = new char[mapHeight, mapWidth];

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i, j] = lines[i][j];
                }
            }
        }

        public void DisplayMap()
        {
        }

        private void DrawBorder()
        {
        }

        public bool WithinBounds(int x, int y)
        {
        }

        private void SetTextColor(char textType)
        {
        }
    }
}

