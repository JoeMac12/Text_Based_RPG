using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class HUD
{
    private Player player;
    private Enemy enemy;
    private Enemy fastEnemy;
    private Enemy straightLineEnemy;
    private int goldScore;
    private string actionMessage;

    private int hudStartPosX;
    private int hudStartPosY;
    private int hudHeight;
    private int actionMessageHeight = 1;

    public HUD(Player player, Enemy enemy, Enemy fastEnemy, Enemy straightLineEnemy, int startX, int startY, int height) // Initialize Hud
    {
        this.player = player;
        this.enemy = enemy;
        this.fastEnemy = fastEnemy;
        this.straightLineEnemy = straightLineEnemy;
        this.hudStartPosX = startX;
        this.hudStartPosY = startY;
        this.hudHeight = height;
    }

    public void UpdateGoldScore(int score) // Update the current gold score
    {
        goldScore = score;
    }

    public void SetActionMessage(string message) // Update action message
    {
        actionMessage = message;
        DisplayActionMessage();
    }

    private void DisplayActionMessage() // Display the message
    {
        int actionMessageStartY = hudStartPosY + hudHeight + actionMessageHeight;
        Console.SetCursorPosition(hudStartPosX, actionMessageStartY);
        Console.Write(new string(' ', Console.WindowWidth)); // Clear the line each time for new messages
        Console.SetCursorPosition(hudStartPosX, actionMessageStartY);
        Console.WriteLine(actionMessage);
    }

    public void Display() // Display game stats
    {
        ClearHUD();
        Console.SetCursorPosition(hudStartPosX, hudStartPosY); // Keep it clear

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Player Health: {player.Health}"); // Player health
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Player Shield: {player.Shield}"); // Player Shield
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow; // Gold
        Console.WriteLine($"Gold: {goldScore} / 10");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red; // Normal Enemy
        Console.WriteLine($"Enemy Health: {enemy.Health}");
        Console.ForegroundColor = ConsoleColor.Cyan; // Fast Enemy
        Console.WriteLine($"Fast Enemy Health: {fastEnemy.Health}");
        Console.ForegroundColor = ConsoleColor.Magenta; // Bouncing Enemy
        Console.WriteLine($"Bouncing Enemy Health: {straightLineEnemy.Health}");
        Console.ResetColor();

        DisplayActionMessage(); // Update action message last
    }

    public void ClearHUD() // Clear hud to prevent overlap
    {
        for (int i = 0; i < hudHeight + actionMessageHeight + 1; i++) // Space out action message
        {
            Console.SetCursorPosition(hudStartPosX, hudStartPosY + i);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}
