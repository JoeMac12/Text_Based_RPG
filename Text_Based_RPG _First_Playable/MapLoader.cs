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

        public MapLoader(string fileName)
        {
            LoadMap(fileName);
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

        public void DisplayMap((int x, int y) playerPosition, (int x, int y) enemyPosition, ConsoleColor playerColor, ConsoleColor enemyColor)
        {
            Console.Clear();
            DrawBorder();

            for (int i = 0; i < mapHeight; i++)
            {
                Console.Write("|");

                for (int j = 0; j < mapWidth; j++)
                {
                    if (i == playerPosition.y && j == playerPosition.x)
                    {
                        Console.ForegroundColor = playerColor;
                        Console.Write('█');
                    }
                    else if (i == enemyPosition.y && j == enemyPosition.x)
                    {
                        Console.ForegroundColor = enemyColor;
                        Console.Write('█');
                    }
                    else
                    {
                        SetTextColor(map[i, j]);
                        Console.Write(map[i, j]);
                    }
                    Console.ResetColor();
                }

                Console.WriteLine("|");
            }

            DrawBorder();
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

