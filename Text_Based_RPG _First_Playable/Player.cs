using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Player
{
    public (int x, int y) Position { get; private set; }
    private HealthSystem healthSystem;
    private Map map;

    public Player(Map map, int initialHealth, int startX, int startY)
    {
        this.map = map;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth);
    }

    public void Move(int deltaX, int deltaY)
    {
        int newX = Position.x + deltaX;
        int newY = Position.y + deltaY;

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY))
        {
            Position = (newX, newY);
        }
    }

    private bool CanMove(int x, int y)
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(1);
    }

    public void TakeDamage(int amount)
    {
        healthSystem.TakeDamage(amount);
    }

    public int Health => healthSystem.Health;
}
