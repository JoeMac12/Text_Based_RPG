using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Map
{
    public char[,] map;
    public int mapHeight { get; private set; }
    public int mapWidth { get; private set; }

    public Map(string fileName) // LoadMap method
    {
        LoadMap(fileName);
    }

    private void LoadMap(string fileName) // Read the map file
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

    public void DisplayMap((int, int) playerPosition, (int, int) enemyPosition, int enemyHealth, (int, int) fastEnemyPosition, int fastEnemyHealth, (int, int) straightLineEnemyPosition, int straightLineEnemyHealth, int startX, int startY) // Display map and everything else on it
    {
        Console.SetCursorPosition(startX, startY); // Keep it clear
        DrawBorder(); // Start drawing the border

        for (int i = 0; i < mapHeight; i++)
        {
            Console.Write("|");
            for (int j = 0; j < mapWidth; j++)
            {
                if (i == playerPosition.Item2 && j == playerPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('█'); // Player icon
                }
                else if (i == straightLineEnemyPosition.Item2 && j == straightLineEnemyPosition.Item1 && straightLineEnemyHealth > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write('█'); // Straight Line Enemy icon
                }
                else if (i == fastEnemyPosition.Item2 && j == fastEnemyPosition.Item1 && fastEnemyHealth > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('█'); // Fast Enemy icon
                }
                else if (i == enemyPosition.Item2 && j == enemyPosition.Item1 && enemyHealth > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('█'); // Enemy icon
                }
                else
                {
                    SetTextColor(map[i, j]); // Set the tile colors
                    Console.Write(map[i, j]);
                }
                Console.ResetColor();
            }
            Console.WriteLine("|");
        }

        DrawBorder(); // Other border
    }

    private void DrawBorder() // Draw border method
    {
        Console.Write("+");
        for (int i = 0; i < mapWidth; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
    }

    public bool WithinBounds(int x, int y) // Check if within the map
    {
        return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
    }

    private void SetTextColor(char textType) // Set the colors
    {
        switch (textType)
        {
            case '♦':
                Console.ForegroundColor = ConsoleColor.Blue; // Teleport item
                break;
            case '♥':
                Console.ForegroundColor = ConsoleColor.Green; // Health item
                break;
            case '♜':
                Console.ForegroundColor = ConsoleColor.Cyan; // Shield item
                break;
            case '.':
                Console.ForegroundColor = ConsoleColor.Black; // Floor / Background
                break;
            case '~':
                Console.ForegroundColor = ConsoleColor.DarkGreen; // Acid
                break;
            case '#':
            case '|':
            case '-':
                Console.ForegroundColor = ConsoleColor.DarkRed; // Walls
                break;
            case 'Θ':
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Gold
                break;
            default:
                Console.ResetColor();
                break;
        }
    }
}
