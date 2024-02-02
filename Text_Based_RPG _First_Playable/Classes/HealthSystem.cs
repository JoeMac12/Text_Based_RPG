using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class HealthSystem
{
    public int Health { get; private set; } // Get the health

    public HealthSystem(int initialHealth) // For setting the initial health at the start of the game
    {
        Health = initialHealth;
    }

    public void TakeDamage(int amount) // Simple take damage method
    {
        Health -= amount;
        if (Health < 0) Health = 0; // Incase health somehow becomes -1
    }
}
