using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
        private Settings settings;

        public GameManager() // Main
        {
            Console.CursorVisible = false;
            settings = new Settings();
            InitializeGame();
        }

        private void InitializeGame() // Initialize the game 
        {
            map = new Map("mapArea.txt"); // Load map

            // Initialize player and enemies

            player = new Player(map,
                                initialHealth: settings.PlayerStartingHealth,
                                startX: settings.PlayerStartingX,
                                startY: settings.PlayerStartingY,
                                initialShield: settings.PlayerStartingShield,
                                settings);

            enemy = new NormalEnemy(map,
                              initialHealth: settings.NormalEnemyStartingHealth,
                              startX: settings.NormalEnemyStartingX,
                              startY: settings.NormalEnemyStartingY,
                              damage: settings.NormalEnemyDamage);

            fastEnemy = new FastEnemy(map,
                              initialHealth: settings.FastEnemyStartingHealth,
                              startX: settings.FastEnemyStartingX,
                              startY: settings.FastEnemyStartingY,
                              damage: settings.FastEnemyDamage);

            straightLineEnemy = new StraightLineEnemy(map,
                              initialHealth: settings.StraightLineEnemyStartingHealth,
                              startX: settings.StraightLineEnemyStartingX,
                              startY: settings.StraightLineEnemyStartingY,
                              damage: settings.StraightLineEnemyDamage);

            // Setup HUD and gold collection

            hud = new HUD(player, enemy, fastEnemy, straightLineEnemy, 0, map.mapHeight + 2, 5);
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

        private void PlayerMovement() // Grab from player class now
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            player.HandleMovement(keyInfo, hud, enemy, fastEnemy, straightLineEnemy);
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
