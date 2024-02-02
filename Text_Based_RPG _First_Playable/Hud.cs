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

    public HUD(Player player, Enemy enemy) // Player health / Enemy health
    {
        this.player = player;
        this.enemy = enemy;
    }

    public void UpdateGoldScore(int score) // Update score when picking up gold
    {
        goldScore = score;
    }

    public void SetActionMessage(string message) // Simple message system for telling the player what happend
    {
        actionMessage = message;
    }

    public void Display() // Main info display
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Player Health: {player.Health}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Enemy Health: {enemy.Health}");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Gold: {goldScore} / 10");
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Action: {actionMessage}");
        Console.ResetColor();
    }
}
