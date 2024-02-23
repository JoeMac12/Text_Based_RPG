using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

// TextBased RPG
// Made by Joseph

// Prob some of the worst code with C# so far lol

namespace Text_Based_RPG__First_Playable
{
    internal class Program
    {
        static void Main(string[] args) // Initialize 
        {
            Map map = new Map("mapArea.txt");
            Player player = new Player(map, initialHealth: 20, startX: map.mapWidth - 78, startY: map.mapHeight - 19, 10);
            Enemy enemy = new Enemy(map, initialHealth: 10, startX: map.mapWidth - 50, startY: map.mapHeight - 3);
            FastEnemy fastEnemy = new FastEnemy(map, initialHealth: 5, startX: map.mapWidth - 35, startY: map.mapHeight - 10);
            StraightLineEnemy straightLineEnemy = new StraightLineEnemy(map, initialHealth: 8, startX: map.mapWidth - 60, startY: map.mapHeight - 15);
            HUD hud = new HUD(player, enemy);
            GoldCollection goldCollection = new GoldCollection(map, hud);
            GameState gameState = new GameState(player, enemy, goldCollection);

            while (!gameState.IsGameOver) // Main game loop
            {
                Console.Clear();

                map.DisplayMap(player.Position, enemy.Position, enemy.Health, fastEnemy.Position, fastEnemy.Health, straightLineEnemy.Position, straightLineEnemy.Health); // Display game map with everything else
                hud.Display();

                player.HasMoved = false; // Set to false at initialize

                PlayerMovement(player, map, hud, enemy, fastEnemy, straightLineEnemy); // Handle player movement

                if (player.HasMoved)
                {
                    if (enemy.Health > 0) // Move the normal enemy only if it is alive
                    {
                        enemy.MoveRandomly(player, hud);
                    }
                    fastEnemy.MoveRandomly(player, hud); // Always move fast enemy since it wont be based on the current state of the normal enemy
                    straightLineEnemy.MoveRandomly(player, hud);
                }

                goldCollection.CheckForGold(player.Position.x, player.Position.y); // Check for gold pickup

                gameState.Update(); // Update the game state
            }

            Console.WriteLine("Press any key to exit."); // Game over / win screen
            Console.ReadKey();
        }

        static void PlayerMovement(Player player, Map map, HUD hud, Enemy enemy, FastEnemy fastEnemy, StraightLineEnemy straightLineEnemy) // Controls for player movement
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

            player.Move(moveX, moveY, hud, enemy, fastEnemy, straightLineEnemy);
        }
    }
}
