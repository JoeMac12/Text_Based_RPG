using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Map
{
    public char[,] map;
    public int Height { get; private set; } // use set in containing class
    public int Width { get; private set; }

    public Map(string fileName)
    {
        LoadMap(fileName);
    }

    private void LoadMap(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        Height = lines.Length;
        Width = lines[0].Length;
        map = new char[Height, Width];

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                map[i, j] = lines[i][j];
            }
        }
    }

    public void DisplayMap()
    {
        DrawBorder();

        for (int i = 0; i < Height; i++)
        {
            Console.Write("|");
            for (int j = 0; j < Width; j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine("|");
        }

        DrawBorder();
    }

    private void DrawBorder()
    {
        Console.Write("+");
        for (int i = 0; i < Width; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
    }

    public bool WithinBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public void SetTextColor(char textType)
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

