using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

internal class HUD // Refence stuff
{
    private Player player;
    private Enemy lastEncounteredEnemy;
    private int goldScore;
    private string actionMessage;

    private int hudStartPosX;
    private int hudStartPosY;
    private int hudHeight;
    private int actionMessageHeight = 1;

    public HUD(int startX, int startY, int height) // Main constructor
    {
        hudStartPosX = startX;
        hudStartPosY = startY;
        hudHeight = height;
    }

    public void SetPlayer(Player player) // set player info
    {
        this.player = player;
    }

    public void UpdateLastEncounteredEnemy(Enemy enemy) // Get last enemy 
    {
        lastEncounteredEnemy = enemy;
    }

    public void UpdateGoldScore(int score) // Update score
    {
        goldScore = score;
    }

    public void SetActionMessage(string message) // Update action message
    {
        actionMessage = message;
    }

    public void Display() // Quick display
    {
        ClearHUD();
        DisplayStats();
        Console.SetCursorPosition(hudStartPosX, hudStartPosY + hudHeight + actionMessageHeight);
        Console.WriteLine(actionMessage);
    }

    private void DisplayStats() // Display game stats
    {
        Console.SetCursorPosition(hudStartPosX, hudStartPosY);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Player Health: {player.Health}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Player Shield: {player.Shield}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Gold: {goldScore} / 10");
        Console.ResetColor();

        if (lastEncounteredEnemy != null) // Check for not null
        {
            Console.ForegroundColor = GetEnemyColor(lastEncounteredEnemy);
            Console.WriteLine($"{lastEncounteredEnemy.GetType().Name} Health: {lastEncounteredEnemy.Health}"); // Only display last enemy touched / attacked
            Console.ResetColor();
        }
    }

    private ConsoleColor GetEnemyColor(Enemy enemy) // Setting enemy colors
    {
        if (enemy is FastEnemy) return ConsoleColor.Cyan;
        if (enemy is StraightLineEnemy) return ConsoleColor.Magenta;
        return ConsoleColor.Red; // Default for normal enemy

    }

    public void ClearHUD() // Clear hud to prevent overlap
    {
        for (int i = 0; i < hudHeight + actionMessageHeight + 1; i++)
        {
            ClearConsoleLine(hudStartPosY + i);
        }
    }

    private void ClearConsoleLine(int line)
    {
        Console.SetCursorPosition(hudStartPosX, line);
        Console.Write(new string(' ', Console.WindowWidth));
    }
}
