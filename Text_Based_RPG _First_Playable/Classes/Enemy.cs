using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Enemy // Initialize 
{
    public (int x, int y) Position { get; private set; } // Initialize and get enemy pos
    private HealthSystem healthSystem;
    private Map map;
    private Random random;

    public Enemy(Map map, int initialHealth, int startX, int startY) // Main method
    {
        this.map = map;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth);
        random = new Random();
    }

    public virtual void MoveRandomly(Player player, HUD hud) // Allows the enemy to move to a random spot, also checks if moving into player, also make it usable for other enemy types
    {
        int direction = random.Next(4); // 1/4 chance to move up,down,left,right
        int x = 0, y = 0;

        switch (direction)
        {
            case 0: y = -1; break;
            case 1: y = 1; break;
            case 2: x = -1; break;
            case 3: x = 1; break;
        }

        int newX = Position.x + x;
        int newY = Position.y + y;

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY)) // Check if enemy is within the map
        {
            if (newX == player.Position.x && newY == player.Position.y) // Moving into the player
            {
                Attack(player);
                hud.SetActionMessage("You took 1 damage from the enemy");
            }
            else
            {
                Position = (newX, newY); // Normal move
            }
        }
    }

    private bool CanMove(int x, int y) // Check if the enemy can move
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    public void Attack(Player player) // Attacking the player
    {
        player.TakeDamage(1);
    }

    public void TakeDamage(int amount, HUD hud) // Reciving damage and checking if died
    {
        healthSystem.TakeDamage(amount);
        if (healthSystem.Health <= 0)
        {
            hud.SetActionMessage("Enemy has died");
            Position = (-1, -1); // Move off map
        }
    }

    public int Health => healthSystem.Health;
}
