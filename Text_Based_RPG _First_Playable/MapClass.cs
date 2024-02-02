using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Map
{
    public char[,] map;
    public int mapHeight;
    public int mapWidth;

    public Map(string fileName)
    {
        LoadMap(fileName);
    }

    private void LoadMap(string fileName)
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

    public void DisplayMap((int, int) playerPosition, (int, int) enemyPosition, int enemyHealth)
    {
        DrawBorder();

        for (int i = 0; i < mapHeight; i++)
        {
            Console.Write("|");

            for (int j = 0; j < mapWidth; j++)
            {
                if (i == playerPosition.Item2 && j == playerPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('█');
                }
                else if (enemyHealth > 0 && i == enemyPosition.Item2 && j == enemyPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
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
        Console.Write("+");
        for (int i = 0; i < mapWidth; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
    }

    private bool WithinBounds(int x, int y)
    {
        return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
    }

    private void SetTextColor(char textType)
    {
        switch (textType)
        {
            case '.': // Floor / Background
                Console.ForegroundColor = ConsoleColor.Black;
                break;
            case '~': // Acid
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                break;
            case '#': // Walls
            case '|':
            case '-':
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
            case 'Θ': // Gold
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                break;
            default:
                Console.ResetColor();
                break;
        }
    }
}
