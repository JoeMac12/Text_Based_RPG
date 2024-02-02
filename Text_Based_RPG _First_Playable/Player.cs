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

    public bool HasMoved { get; set; }

    public Player(Map map, int initialHealth, int startX, int startY)
    {
        this.map = map;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth);
        HasMoved = false;
    }

    public void Move(int moveX, int moveY)
    {
        int newX = Position.x + moveX;
        int newY = Position.y + moveY;

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY))
        {
            HasMoved = true;
            Position = (newX, newY);
        }
        else
        {
            HasMoved = false;
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
