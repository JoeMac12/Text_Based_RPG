using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class HealthSystem
{
    public int Health { get; private set; } // Get health
    public int Shield { get; private set; } // Get shield

    public HealthSystem(int initialHealth, int initialShield = 0) // For setting the initial health and shield at the start of the game
    {
        Health = initialHealth;
        Shield = initialShield;
    }

    public void TakeDamage(int amount) // Simple take damage method
    {
        int damageAfterShield = amount - Shield;
        Shield -= amount;

        if (Shield < 0)
        {
            Shield = 0;
            if (damageAfterShield > 0)
            {
                Health -= damageAfterShield;
            }
        }

        if (Health < 0) Health = 0;
    }

    public void AddShield(int amount) // Simple regen shield for pickup
    {
        Shield += amount;
    }

    public void AddHealth(int amount) // Simple regen for health
    {
        Health += amount;
    }
}
