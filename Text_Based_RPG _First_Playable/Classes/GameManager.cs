using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class GameManager // Classes
    {
        private Map map;
        private Player player;
        private Enemy enemy;
        private FastEnemy fastEnemy;
        private StraightLineEnemy straightLineEnemy;
        private HUD hud;
        private GoldCollection goldCollection;
        private GameState gameState;

        public GameManager() // Main
        {
            Console.CursorVisible = false;
            InitializeGame();
        }

        private void InitializeGame() // Initialize the game 
        {
            map = new Map("mapArea.txt"); // Load map

            // Initialize player and enemies

            player = new Player(map, initialHealth: 20, startX: map.mapWidth - 78, startY: map.mapHeight - 19, initialShield: 10);
            enemy = new Enemy(map, initialHealth: 10, startX: map.mapWidth - 50, startY: map.mapHeight - 3);
            fastEnemy = new FastEnemy(map, initialHealth: 5, startX: map.mapWidth - 35, startY: map.mapHeight - 10);
            straightLineEnemy = new StraightLineEnemy(map, initialHealth: 8, startX: map.mapWidth - 60, startY: map.mapHeight - 15);

            // Setup HUD and gold collection

            hud = new HUD(player, enemy, 0, map.mapHeight + 2, 5);
            goldCollection = new GoldCollection(map, hud);

            // Initialize game state

            gameState = new GameState(player, enemy, goldCollection);
        }

        public void StartGameLoop() // Main game loop
        {
            int mapStartPosX = Console.CursorLeft; // For keeping things tidy
            int mapStartPosY = Console.CursorTop;

            while (!gameState.IsGameOver) // While game is active
            {
                map.DisplayMap(player.Position, enemy.Position, enemy.Health, fastEnemy.Position, fastEnemy.Health, straightLineEnemy.Position, straightLineEnemy.Health, mapStartPosX, mapStartPosY); // Display the whole map with everything on it
                hud.ClearHUD();
                hud.Display();

                player.HasMoved = false; // Set to false at the start of the game

                PlayerMovement(); // Call player movement

                if (player.HasMoved)
                {
                    MoveEnemies(); // Move enemies if the player has moved
                }

                goldCollection.CheckForGold(player.Position.x, player.Position.y); // Check for gold pickup

                gameState.Update(); // Update the game state each movement
            }

            EndGame(); // End the game Win or Lose
        }

        private void PlayerMovement() // Player controls
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int moveX = 0, moveY = 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.W: // Up
                case ConsoleKey.UpArrow:
                    moveY = -1;
                    break;
                case ConsoleKey.S: // Down
                case ConsoleKey.DownArrow:
                    moveY = 1;
                    break;
                case ConsoleKey.A: // Left
                case ConsoleKey.LeftArrow:
                    moveX = -1;
                    break;
                case ConsoleKey.D: // Right
                case ConsoleKey.RightArrow:
                    moveX = 1;
                    break;
            }

            player.Move(moveX, moveY, hud, enemy, fastEnemy, straightLineEnemy); // Update and check for each player movement
        }

        private void MoveEnemies() // Moving the enemies
        {
            enemy.MoveRandomly(player, hud);
            fastEnemy.MoveRandomly(player, hud);
            straightLineEnemy.MoveRandomly(player, hud);
        }

        private void EndGame() // End the game
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
