using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class HUD
{
    private Player player;
    private int goldScore;
    private string actionMessage;

    public HUD(Player player)
    {
        this.player = player;
        this.goldScore = 0;
        this.actionMessage = "";
    }

    public void UpdateGoldScore(int score)
    {
        goldScore = score;
    }

    public void SetActionMessage(string message)
    {
        actionMessage = message;
    }

    public void Display()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Player Health: {player.Health}");
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
