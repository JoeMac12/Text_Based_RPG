using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

internal class Player
{
    public (int x, int y) Position { get; private set; }
    private HealthSystem healthSystem;
    private Map map;

    public bool HasMoved { get; set; }

    public Player(Map map, int initialHealth, int startX, int startY, int initialShield = 0) // Initialize the player
    {
        this.map = map;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth, initialShield);
        HasMoved = false;
    }

    public void HandleMovement(ConsoleKeyInfo keyInfo, HUD hud, Enemy enemy, FastEnemy fastEnemy, StraightLineEnemy straightLineEnemy)
    {
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

        Move(moveX, moveY, hud, enemy, fastEnemy, straightLineEnemy);
    }

    public void Move(int moveX, int moveY, HUD hud, Enemy enemy, FastEnemy fastEnemy, StraightLineEnemy straightLineEnemy) // Main player move method
    {
        int newX = Position.x + moveX;
        int newY = Position.y + moveY;

        // Attempt attacking the enemies

        bool enemyAttacked = CheckCollisionAndAttack(newX, newY, enemy, hud, "You dealt 1 damage to the enemy!") ||
                             CheckCollisionAndAttack(newX, newY, fastEnemy, hud, "You dealt 1 damage to the fast enemy!") ||
                             CheckCollisionAndAttack(newX, newY, straightLineEnemy, hud, "You dealt 1 damage to the bouncing enemy!");

        if (enemyAttacked) // Prevent moving if an enemy is attacked
        {
            HasMoved = true; // Trigger move when attacking
            return; // Don't move while attacking
        }

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY)) // Make player move if not attacking and the move is valid
        {
            Position = (newX, newY); // Update player position
            HasMoved = true;
            CheckTile(hud, newX, newY); // Check the current tile
        }
        else
        {
            HasMoved = false; // False movement
        }
    }

    private bool CheckCollisionAndAttack(int newX, int newY, Enemy enemy, HUD hud, string message) // Check enemy collison and attack if possible
    {
        if (newX == enemy.Position.x && newY == enemy.Position.y && enemy.Health > 0)
        {
            enemy.TakeDamage(1, hud);
            hud.SetActionMessage(message);
            return true; // Set to true if encountered and attacked a enemy
        }
        return false; // No enemy was encountered
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
            case '♜': // Shield
                RegenerateShield(5);
                map.map[y, x] = '.'; // Replace with floor
                hud.SetActionMessage("Your shield has been increased by 5 HP!");
                break;
            case '♥': // Health
                RegenerateHealth(5);
                map.map[y, x] = '.'; // Replace with floor
                hud.SetActionMessage("Your health has been increased by 5 HP!");
                break;
            case '♦': // Teleport
                TeleportRandomly();
                map.map[y, x] = '.'; // Replace with floor
                hud.SetActionMessage("You have been teleported to a random location!");
                break;
        }
    }

    public void TakeDamage(int amount) // Take Damage via Health System
    {
        healthSystem.TakeDamage(amount);
    }

    public void RegenerateShield(int amount) // Regen Shield via Health System
    {
        healthSystem.AddShield(amount);
    }

    public void RegenerateHealth(int amount) // Regen Health via Health System
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
