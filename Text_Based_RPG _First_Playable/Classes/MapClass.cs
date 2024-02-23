using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Map // Initialize 
{
    public char[,] map;
    public int mapHeight { get; private set; }
    public int mapWidth { get; private set; }

    public Map(string fileName) // Map method
    {
        LoadMap(fileName);
    }

    private void LoadMap(string fileName) // Main load method
    {
        string[] lines = File.ReadAllLines(fileName); // Read mapArea.txt
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

    public void DisplayMap((int, int) playerPosition, (int, int) enemyPosition, int enemyHealth, (int, int) fastEnemyPosition, int fastEnemyHealth, (int, int) straightLineEnemyPosition, int straightLineEnemyHealth) // Displays the map and everything on it
    {
        DrawBorder(); // Draw top border

        for (int i = 0; i < mapHeight; i++)
        {
            Console.Write("|");

            for (int j = 0; j < mapWidth; j++)
            {
                if (i == playerPosition.Item2 && j == playerPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Player
                    Console.Write('█');
                }
                else if (straightLineEnemyHealth > 0 && i == straightLineEnemyPosition.Item2 && j == straightLineEnemyPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; // Straight Line Enemy
                    Console.Write('█');
                }
                else if (fastEnemyHealth > 0 && i == fastEnemyPosition.Item2 && j == fastEnemyPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.Blue; // Fast Enemy
                    Console.Write('█');
                }
                else if (enemyHealth > 0 && i == enemyPosition.Item2 && j == enemyPosition.Item1)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Normal Enemy
                    Console.Write('█');
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

        DrawBorder(); // Draw bottem border
    }

    private void DrawBorder() // Main draw border method
    {
        Console.Write("+");
        for (int i = 0; i < mapWidth; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
    }

    public bool WithinBounds(int x, int y) // Simple check to see if player or enemy is within the map
    {
        return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
    }

    private void SetTextColor(char textType) // Sets the colors for each tile type
    {
        switch (textType)
        {
            case '✧': // Teleport item
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case '♥': // Health item
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case '♜': // Shield item
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
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
