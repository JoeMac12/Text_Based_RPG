using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class HealthSystem
{
    public int Health { get; private set; }

    public HealthSystem(int initialHealth)
    {
        Health = initialHealth;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0; // Adding this incase health somehow becomes -1
    }

    public void Heal(int amount) // Unused atm
    {
        Health += amount;
    }
}
