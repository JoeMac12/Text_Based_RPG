using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
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

        public void LoadMap(string fileName) // Reads the lines of the loaded map file
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

        public void DisplayMap((int x, int y) playerPosition, (int x, int y) enemyPosition, ConsoleColor playerColor, ConsoleColor enemyColor) // Displays all the content of the game map
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

        public void DrawBorder() // Draws a border around the map
        {
            Console.Write("+");
            for (int i = 0; i < mapWidth; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }

        public bool WithinBounds(int x, int y) // Used for checking if the player is within the map bounds
        {
            return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
        }

        public void SetTextColor(char textType) // Sets the color for each map tile type
        {
            switch (textType)
            {
                case '.':
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case '~':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case '#':
                case '|':
                case '-':
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 'Θ':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }

        //  Simple way to check for what is at a certain location or to change a tile to another type
        //  Only used for gold atm

        public char GetMapTile(int x, int y)
        {
            if (WithinBounds(x, y))
            {
                return map[y, x];
            }
            else
            {
                return ' ';
            }
        }

        public void UpdateMapTile(int x, int y, char newTile)
        {
            if (WithinBounds(x, y))
            {
                map[y, x] = newTile;
            }
        }
    }
}
