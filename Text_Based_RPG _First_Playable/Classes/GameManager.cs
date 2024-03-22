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
        private HUD hud;
        private GoldCollection goldCollection;
        private GameState gameState;
        private Settings settings;
        private EnemyManager enemyManager;

        public GameManager() // Main
        {
            Console.CursorVisible = false;
            settings = new Settings();
            InitializeGame();
        }

        private void InitializeGame() // Initialize the game 
        {
            map = new Map("mapArea.txt"); // Load map
            hud = new HUD(0, map.mapHeight + 2, 5); // Hud
            enemyManager = new EnemyManager(map, hud); // Enemy Manager

             // Initialize player and enemies

             player = new Player(map,
                                initialHealth: settings.PlayerStartingHealth,
                                startX: settings.PlayerStartingX,
                                startY: settings.PlayerStartingY,
                                initialShield: settings.PlayerStartingShield,
                                settings);

            hud.SetPlayer(player); // Set hud stats for player

            // Enemies

            enemyManager.AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 20, 15, settings.NormalEnemyDamage));
            enemyManager.AddEnemy(new FastEnemy(map, settings.FastEnemyStartingHealth, 31, 10, settings.FastEnemyDamage));
            enemyManager.AddEnemy(new StraightLineEnemy(map, settings.StraightLineEnemyStartingHealth, 70, 15, settings.StraightLineEnemyDamage));


            // I gotta add 25 more :)



            // Setup HUD and gold collection

            goldCollection = new GoldCollection(map, hud);

            // Initialize game state

            gameState = new GameState(player, goldCollection);
        }

        public void StartGameLoop() // Main game loop
        {
            int mapStartPosX = Console.CursorLeft; // For keeping things tidy
            int mapStartPosY = Console.CursorTop;

            while (!gameState.IsGameOver) // While game is active
            {
                map.DisplayMap(player.Position, enemyManager, mapStartPosX, mapStartPosY); // Display the whole map with everything on it
                hud.ClearHUD();
                hud.Display();

                player.HasMoved = false; // Set to false at the start of the game

                PlayerMovement(); // Call player movement

                if (player.HasMoved)
                {
                    enemyManager.UpdateAll(player); // Move all enemies
                }

                goldCollection.CheckForGold(player.Position.x, player.Position.y); // Check for gold pickup

                gameState.Update(); // Update the game state each movement
            }

            EndGame(); // End the game Win or Lose
        }

        private void PlayerMovement() // Grab from player class now
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            player.HandleMovement(keyInfo, hud, enemyManager);
        }

        private void EndGame() // End the game
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
