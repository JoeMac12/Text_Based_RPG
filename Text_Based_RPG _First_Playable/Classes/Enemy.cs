﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Enemy
{
    public (int x, int y) Position { get; protected set; }
    private HealthSystem healthSystem;
    protected Map map;
    private Random random;
    protected int damage;

    public Enemy(Map map, int initialHealth, int startX, int startY, int damage)
    {
        this.map = map;
        this.damage = damage;
        Position = (startX, startY);
        healthSystem = new HealthSystem(initialHealth);
        random = new Random();
    }

    public virtual void MoveRandomly(Player player, HUD hud)
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
                hud.SetActionMessage("You took 2 damage from normal enemy");
            }
            else
            {
                Position = (newX, newY);
            }
        }
    }

    protected bool CanMove(int x, int y)
    {
        char tile = map.map[y, x];
        return tile != '#' && tile != '|' && tile != '-';
    }

    public void Attack(Player player)
    {
        player.TakeDamage(damage);
    }

    public void TakeDamage(int amount, HUD hud)
    {
        healthSystem.TakeDamage(amount);
        if (healthSystem.Health <= 0)
        {
            hud.SetActionMessage("Enemy has died");
            Position = (-1, -1);
        }
    }

    public int Health => healthSystem.Health;
}
