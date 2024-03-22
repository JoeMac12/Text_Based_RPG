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
        this.hudStartPosX = startX;
        this.hudStartPosY = startY;
        this.hudHeight = height;
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
        DisplayActionMessage();
    }

    public void Display() // Quick display
    {
        ClearHUD();
        DisplayStats();
        DisplayActionMessage();
    }

    private void DisplayStats() // Display game stats
    {
        Console.SetCursorPosition(hudStartPosX, hudStartPosY); 

        if (player != null) // Check not null just cause
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Player Health: {player.Health}"); // Player health
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Player Shield: {player.Shield}"); // Player Shield
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow; // Gold
        Console.WriteLine($"Gold: {goldScore} / 10");
        Console.ResetColor();

        if (lastEncounteredEnemy != null) // Again check for not null
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

    private void DisplayActionMessage() // Display the message
    {
        int actionMessageStartY = hudStartPosY + hudHeight + actionMessageHeight;
        Console.SetCursorPosition(hudStartPosX, actionMessageStartY);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(hudStartPosX, actionMessageStartY);
        Console.WriteLine(actionMessage);
    }

    public void ClearHUD() // Clear hud to prevent overlap
    {
        for (int i = 0; i < hudHeight + actionMessageHeight + 1; i++)
        {
            Console.SetCursorPosition(hudStartPosX, hudStartPosY + i);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}
