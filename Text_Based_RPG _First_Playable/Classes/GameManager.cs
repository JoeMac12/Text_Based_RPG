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

        private int mapStartPosX;
        private int mapStartPosY;

        public GameManager() // Main 
        {
            Console.CursorVisible = false;
            settings = new Settings();
            Init();
        }

        private void Init() // Initialize the game 
        {
            map = new Map("mapArea.txt"); // Initialize and Load map
            hud = new HUD(0, map.mapHeight + 2, 5);
            enemyManager = new EnemyManager(map, hud, settings); // Enemy manager and initialize enemies

            // Initialize player
            player = new Player(map, 
                initialHealth: settings.PlayerStartingHealth, 
                startX: settings.PlayerStartingX, 
                startY: settings.PlayerStartingY, 
                initialShield: settings.PlayerStartingShield, 
                settings); 

            hud.SetPlayer(player); // Initialize hud stats for player
            goldCollection = new GoldCollection(map, hud); // Initialize HUD and gold collection
            gameState = new GameState(player, goldCollection); // Initialize game state

            mapStartPosX = Console.CursorLeft;
            mapStartPosY = Console.CursorTop;
        }

        private void Draw() // Display game elements
        {
            map.DisplayMap(player.Position, enemyManager, mapStartPosX, mapStartPosY); // Display the whole map with everything on it
            hud.ClearHUD();
            hud.Display();
        }

        private void Update() // Update game
        {
            player.HasMoved = false;

            PlayerMovement(); // Move Player

            if (player.HasMoved)
            {
                enemyManager.UpdateAll(player); // Move all enemies
            }

            goldCollection.CheckForGold(player.Position.x, player.Position.y); // Check for gold pickup
            gameState.Update(); // Update the game state each movement
        }

        public void StartGameLoop() // Main game loop
        {
            while (!gameState.IsGameOver) // While game is active
            {
                Draw();
                Update();
            }

            EndGame(); // End the game Win or Lose based on state
        }

        private void PlayerMovement() // Grab from player class
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            player.HandleMovement(keyInfo, hud, enemyManager);
        }

        private void EndGame() // End the game
        {
            Console.WriteLine("(Press any key to exit the game)");
            Console.ReadKey(true);
        }
    }
}
