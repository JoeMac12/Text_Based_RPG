using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable
{
    internal class Program
    {
        static void Main(string[] args) // Main 
        {
            Map map = new Map("mapArea.txt");

            Player player = new Player(map, 20, map.mapWidth - 78, map.mapHeight - 19);
            Enemy enemy = new Enemy(map, 10, map.mapWidth - 50, map.mapHeight - 3);

            HUD hud = new HUD(player);
            GoldCollection goldCollection = new GoldCollection(map, hud);

            GameState gameState = new GameState(player, enemy, goldCollection);

            while (!gameState.IsGameOver) // Run main game loop untill game over is true
            {
                Console.Clear(); // Simple clear screen for now

                map.DisplayMap(player.Position, enemy.Position, enemy.Health);
                hud.Display();

                player.HasMoved = false;

                PlayerMovement(player, map);

                if (player.HasMoved && enemy.Health > 0)
                {
                    enemy.MoveRandomly();
                }

                goldCollection.CheckForGold(player.Position.x, player.Position.y);

                gameState.Update();
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void PlayerMovement(Player player, Map map) // Put the player controls here cause it somehow works better?
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int moveX = 0, moveY = 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    moveY = -1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    moveY = 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    moveX = -1;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    moveX = 1;
                    break;
            }

            player.Move(moveX, moveY);
        }
    }
}