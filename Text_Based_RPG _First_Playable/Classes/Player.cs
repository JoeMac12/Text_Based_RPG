﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

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

    public void Move(int moveX, int moveY, HUD hud, Enemy enemy, FastEnemy fastEnemy) // Main method for moving the player with enemy checks and action messages
    {
        int newX = Position.x + moveX;
        int newY = Position.y + moveY;

        if (newX == enemy.Position.x && newY == enemy.Position.y && enemy.Health > 0) // Check collision with normal enemy
        {
            Attack(enemy, hud);
            hud.SetActionMessage("You dealt 1 damage to the enemy");
            return; // Stop Moving
        }

        if (newX == fastEnemy.Position.x && newY == fastEnemy.Position.y && fastEnemy.Health > 0) // Check collision with the fast enemy
        {
            Attack(fastEnemy, hud);
            hud.SetActionMessage("You dealt 1 damage to the fast enemy!");
            return; // Stop Moving
        }

        if (map.WithinBounds(newX, newY) && CanMove(newX, newY))
        {
            HasMoved = true;
            Position = (newX, newY);

            if (map.map[newY, newX] == '~') // Check if Moved into acid
            {
                TakeDamage(1);
                hud.SetActionMessage("You stepped in acid and took 1 damage!");
            }

            if (map.map[newY, newX] == '♜') // Check for shield item
            {
                RegenerateShield(5); // Regen 5 shield
                map.map[newY, newX] = '.'; // replace with background when collected
                hud.SetActionMessage("Your shield has been regenerated by 5 HP!");
            }

            if (map.map[newY, newX] == '♥') // Check for health item
            {
                RegenerateHealth(5); // Regen 5 health
                map.map[newY, newX] = '.'; // replace with background when collected
                hud.SetActionMessage("Your health has been regenerated by 5 HP!");
            }

            if (map.map[newY, newX] == '✧') // Check for teleport item
            {
                TeleportRandomly(); // TP to a random spot
                map.map[newY, newX] = '.'; // replace with background when collected
                hud.SetActionMessage("You have been teleported to a random location!");
            }
        }
        else // Anything else for movement just set to false
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

    public void RegenerateShield(int amount) // Regerate shield
    {
        healthSystem.AddShield(amount);
    }

    public void RegenerateHealth(int amount) // Regerate health
    {
        healthSystem.AddHealth(amount);
    }

    public void TeleportRandomly() // Teleport the player to a random spot on the map
    {
        Random rnd = new Random();
        int x, y;
        do
        {
            x = rnd.Next(map.mapWidth);
            y = rnd.Next(map.mapHeight);
        } while (!CanMove(x, y)); // Make sure the player goes to a movable space

        Position = (x, y); // Send them there
    }

    public int Health => healthSystem.Health;
    public int Shield => healthSystem.Shield;
}
