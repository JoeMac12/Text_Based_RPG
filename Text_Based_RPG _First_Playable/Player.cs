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

    public void Move(int moveX, int moveY, HUD hud, Enemy enemy)
    {
        int newX = Position.x + moveX;
        int newY = Position.y + moveY;

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY))
        {
            if (newX == enemy.Position.x && newY == enemy.Position.y)
            {
                Attack(enemy, hud);
                hud.SetActionMessage("You dealt 1 damage to enemy");
                HasMoved = true;
            }
            else
            {
                HasMoved = true;
                Position = (newX, newY);

                if (map.map[newY, newX] == '~')
                {
                    TakeDamage(1);
                    hud.SetActionMessage("You stepped in acid!"); 
                }
            }
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

    public void Attack(Enemy enemy, HUD hud)
    {
        enemy.TakeDamage(1, hud);
    }

    public void TakeDamage(int amount)
    {
        healthSystem.TakeDamage(amount);
    }

    public int Health => healthSystem.Health;
}
