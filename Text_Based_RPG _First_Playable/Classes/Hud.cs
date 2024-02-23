using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class HUD // Initialize 
{
    private Player player;
    private Enemy enemy;
    private int goldScore;
    private string actionMessage;

    private int hudStartPosX;
    private int hudStartPosY;
    private int hudHeight;
    private int actionMessageHeight = 1;

    public HUD(Player player, Enemy enemy, int startX, int startY, int height)
    {
        this.player = player;
        this.enemy = enemy;
        this.hudStartPosX = startX;
        this.hudStartPosY = startY;
        this.hudHeight = height;
    }

    public void UpdateGoldScore(int score) // Update score when picking up gold
    {
        goldScore = score;
    }

    public void SetActionMessage(string message)
    {
        int actionMessageStartY = hudStartPosY + hudHeight;
        Console.SetCursorPosition(hudStartPosX, actionMessageStartY);

        for (int i = 0; i < actionMessageHeight; i++)
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.CursorTop++;
        }

        Console.SetCursorPosition(hudStartPosX, actionMessageStartY);
        Console.WriteLine(message);
    }


    public void Display() // Main info display
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Player Health: {player.Health}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Player Shield: {player.Shield}"); 
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Enemy Health: {enemy.Health}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Gold: {goldScore} / 10");
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{actionMessage}");
        Console.ResetColor();
    }

    public void ClearHUD()
    {
        Console.SetCursorPosition(hudStartPosX, hudStartPosY);
        for (int i = 0; i < hudHeight; i++)
        {
            Console.Write(new string(' ', Console.WindowWidth));
        }
        Console.SetCursorPosition(hudStartPosX, hudStartPosY);
    }
}
