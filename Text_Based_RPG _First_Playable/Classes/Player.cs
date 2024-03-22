using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

internal class Player // Define
{
    public (int x, int y) Position { get; private set; }
    private HealthSystem healthSystem;
    private Settings settings;
    private Map map;

    public bool HasMoved { get; set; } // For moving checking

    public Player(Map map, int initialHealth, int startX, int startY, int initialShield, Settings settings) // Initialize the player
    {
        this.map = map;
        this.settings = settings;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth, initialShield);
        HasMoved = false;
    }

    public void HandleMovement(ConsoleKeyInfo keyInfo, HUD hud, EnemyManager enemyManager) // Handle player movement here instead of game manager class
    {
        int moveX = 0, moveY = 0;

        switch (keyInfo.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow: // Up
                moveY = -1;
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow: // Down
                moveY = 1;
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow: // Left
                moveX = -1;
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow: // Right
                moveX = 1;
                break;
        }

        while (Console.KeyAvailable) // Clear key buffer 
        {
            Console.ReadKey(true);
        }

        Move(moveX, moveY, hud, enemyManager);
    }

    public void Move(int moveX, int moveY, HUD hud, EnemyManager enemyManager) // Movement method
    {
        int newX = Position.x + moveX;
        int newY = Position.y + moveY;

        foreach (var enemy in enemyManager.Enemies) // Check for collisions with any enemy
        {
            if (CheckCollisionAndAttack(newX, newY, enemy, hud))
            {
                HasMoved = true;
                return; // Stop checking if attacked an enemy
            }
        }

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY)) // Make player move if not attacking and the move is valid
        {
            Position = (newX, newY);
            HasMoved = true;
            CheckTile(hud, newX, newY); // Check tile
        }
        else
        {
            HasMoved = false; // False movement
        }
    }

    private bool CheckCollisionAndAttack(int newX, int newY, Enemy enemy, HUD hud) // Check collisions 
    {
        if (newX == enemy.Position.x && newY == enemy.Position.y && enemy.Health > 0)
        {
            enemy.TakeDamage(1, hud);
            hud.SetActionMessage("You dealt 1 damage to an enemy!");
            hud.UpdateLastEncounteredEnemy(enemy); // Update hud with that enemy attacked
            return true;
        }
        return false;
    }

    private bool CanMove(int x, int y) // Check if can move
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    private void CheckTile(HUD hud, int x, int y) // Check for special tiles and items
    {
        char tile = map.map[y, x];
        switch (tile)
        {
            case '~': // Acid
                TakeDamage(1);
                hud.SetActionMessage("You stepped in acid and took 1 damage!");
                break;
            case '$': // Shield
                RegenerateShield(settings.ShieldRegenAmount);
                map.map[y, x] = '.';
                hud.SetActionMessage($"Your shield has been increased by {settings.ShieldRegenAmount} HP!");
                break;
            case '♥': // Health
                RegenerateHealth(settings.HealthHealAmount);
                map.map[y, x] = '.';
                hud.SetActionMessage($"Your health has been increased by {settings.HealthHealAmount} HP!");
                break;
            case '♦': // Teleport
                TeleportRandomly();
                map.map[y, x] = '.';
                hud.SetActionMessage("You have been teleported to a random location!");
                break;
        }
    }

    // Use health system for these

    public void TakeDamage(int amount) // Take damage
    {
        healthSystem.TakeDamage(amount);
    }

    public void RegenerateShield(int amount) // Regen shield
    {
        healthSystem.AddShield(amount);
    }

    public void RegenerateHealth(int amount) // Regen health
    {
        healthSystem.AddHealth(amount);
    }

    public void TeleportRandomly() // TP to a random spot
    {
        Random rnd = new Random();
        int x, y;
        do
        {
            x = rnd.Next(map.mapWidth);
            y = rnd.Next(map.mapHeight);
        } while (!CanMove(x, y));
        Position = (x, y);
    }

    public int Health => healthSystem.Health;
    public int Shield => healthSystem.Shield;
}
