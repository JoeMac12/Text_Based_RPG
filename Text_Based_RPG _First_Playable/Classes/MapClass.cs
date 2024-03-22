using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

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

    public void DisplayMap((int, int) playerPosition, EnemyManager enemyManager, int startX, int startY) // Display map and everything
    {
        Console.SetCursorPosition(startX, startY);
        DrawBorder();

        for (int i = 0; i < mapHeight; i++)
        {
            Console.Write("|");
            for (int j = 0; j < mapWidth; j++)
            {
                char displayChar = GetDisplayCharForPosition(j, i, playerPosition, enemyManager);
                SetTextColor(displayChar);
                Console.Write(displayChar);
                Console.ResetColor();
            }
            Console.WriteLine("|");
        }

        DrawBorder();
    }

    private char GetDisplayCharForPosition(int x, int y, (int, int) playerPosition, EnemyManager enemyManager) // Define icons and apply correct matching
    {
        if (x == playerPosition.Item1 && y == playerPosition.Item2)
        {
            Console.ForegroundColor = ConsoleColor.Green; // player
            return '█';
        }

        foreach (var enemy in enemyManager.Enemies) // Enemies
        {
            if (x == enemy.Position.x && y == enemy.Position.y && enemy.Health > 0)
            {
                switch (enemy.EnemyType) // Set color based on enemy type
                {
                    case "Normal":
                        Console.ForegroundColor = ConsoleColor.Red; // Normal
                        break;
                    case "Fast":
                        Console.ForegroundColor = ConsoleColor.Cyan; // Fast
                        break;
                    case "StraightLine":
                        Console.ForegroundColor = ConsoleColor.Yellow; // Bouncing 
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White; // Default 
                        break;
                }
                return '█'; // Enemy icon
            }
        }
        return map[y, x];
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
            case '$':
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
                Console.ForegroundColor = ConsoleColor.Magenta; // Walls
                break;
            case 'Θ':
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Gold
                break;
            default:
                break;
        }
    }
}
