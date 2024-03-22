using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal abstract class Enemy // This is the enemy "base" class, used to be normal enemy but thats another class now
{
    public (int x, int y) Position { get; protected set; }
    public abstract string EnemyType { get; } // Used for setting enemy types for colors
    protected HealthSystem healthSystem;
    protected Map map;
    protected static Random random = new Random(); // should fix enemies moving the same everytime
    protected int damage;

    public Enemy(Map map, int initialHealth, int startX, int startY, int damage) // Base 
    {
        this.map = map;
        this.damage = damage;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth);
        random = new Random();
    }

    public virtual void MoveRandomly(Player player, HUD hud) // Base movement
    {
        int direction = random.Next(4);
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

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY))
        {
            if (newX == player.Position.x && newY == player.Position.y) // Check if moving into player
            {
                Attack(player, hud); // Attack
            }
            else
            {
                Position = (newX, newY);
            }
        }
    }

    protected bool CanMove(int x, int y) // If can move to a valid pos
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    public virtual void Attack(Player player, HUD hud) // Base attack method
    {
        player.TakeDamage(damage);
        hud.SetActionMessage($"You took {damage} damage from an enemy");
    }

    public void TakeDamage(int amount, HUD hud) // Base take damage
    {
        healthSystem.TakeDamage(amount);
        if (healthSystem.Health <= 0)
        {
            hud.SetActionMessage("An enemy has died!");
            Position = (-1, -1);
        }
    }

    public int Health => healthSystem.Health;
}
