using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class GameState // Initialize 
{
    private Player player;
    private GoldCollection goldCollection;
    private bool isGameOver;

    public GameState(Player player, GoldCollection goldCollection) // Main State
    {
        this.player = player;
        this.goldCollection = goldCollection;
    }

    public void Update() // Updates after each player input
    {
        CheckWinCondition();
        CheckGameOver();
    }

    private void CheckWinCondition() // If the player wins
    {
        if (goldCollection.GoldScore >= 10)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations!");
            Console.WriteLine();
            Console.WriteLine("You have successfully collected all 10 gold while surviving!");
            Console.WriteLine();
            Console.ResetColor();
            isGameOver = true;
        }
    }

    private void CheckGameOver() // If the player dies
    {
        if (player.Health <= 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have died!");
            Console.WriteLine();
            Console.ResetColor();
            isGameOver = true;
        }
    }

    public bool IsGameOver => isGameOver;
}

