using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Enemy
{
    public (int x, int y) Position { get; private set; }
    private HealthSystem healthSystem;
    private Map map;
    private Random random;

    public Enemy(Map map, int initialHealth, int startX, int startY)
    {
        this.map = map;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth);
        random = new Random();
    }

    public void MoveRandomly(Player player)
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
            if (newX == player.Position.x && newY == player.Position.y)
            {
                Attack(player);
            }
            else
            {
                Position = (newX, newY);
            }
        }
    }

    private bool CanMove(int x, int y)
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    public void Attack(Player player)
    {
        player.TakeDamage(1);
    }

    public void TakeDamage(int amount)
    {
        healthSystem.TakeDamage(amount);
    }

    public int Health => healthSystem.Health;
}
