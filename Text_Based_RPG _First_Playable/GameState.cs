using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class GameState
{
    private Player player;
    private GoldCollection goldCollection;
    private bool isGameOver;

    public GameState(Player player, Enemy enemy, GoldCollection goldCollection)
    {
        this.player = player;
        this.goldCollection = goldCollection;
        this.isGameOver = false;
    }

    public void Update()
    {
        CheckWinCondition();
        CheckGameOver();
    }

    private void CheckWinCondition()
    {
        if (goldCollection.GoldScore >= 10)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have collected all 10 gold coins!");
            Console.WriteLine("You win!");
            Console.ResetColor();
            isGameOver = true;
        }
    }

    private void CheckGameOver()
    {
        if (player.Health <= 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have Died");
            Console.ResetColor();
            isGameOver = true;
        }
    }

    public bool IsGameOver => isGameOver;
}

