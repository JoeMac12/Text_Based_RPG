using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Player
{
    public (int x, int y) Position { get; private set; } // Initialize and get pos
    private HealthSystem healthSystem;
    private Map map;

    public bool HasMoved { get; set; }

    public Player(Map map, int initialHealth, int startX, int startY, int initialShield = 0) // Main method
    {
        this.map = map;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth, initialShield);
        HasMoved = false;
    }

    public void Move(int moveX, int moveY, HUD hud, Enemy enemy) // Main method for moving the player with enemy checks and action messages
    {
        int newX = Position.x + moveX;
        int newY = Position.y + moveY;

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY)) // Check if still within the map and can move
        {
            if (newX == enemy.Position.x && newY == enemy.Position.y) // Moving into the enemy
            {
                Attack(enemy, hud);
                hud.SetActionMessage("You dealt 1 damage to enemy");
                HasMoved = true;
            }
            else // Move to a open space
            {
                HasMoved = true;
                Position = (newX, newY);

                if (map.map[newY, newX] == '~') // Move into acid
                {
                    TakeDamage(1);
                    hud.SetActionMessage("You stepped in acid!"); 
                }
            }
        }
        else // For failed moves like moving into a wall or smt
        {
            HasMoved = false;
        }
    }

    private bool CanMove(int x, int y) // Check if the player can move to an open tile thats not a wall
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    public void Attack(Enemy enemy, HUD hud) // Attacking the enemy
    {
        enemy.TakeDamage(1, hud);
    }

    public void TakeDamage(int amount) // Used for when taking damage from enemy or acid
    {
        healthSystem.TakeDamage(amount);
    }

    public int Health => healthSystem.Health;
    public int Shield => healthSystem.Shield;
}
