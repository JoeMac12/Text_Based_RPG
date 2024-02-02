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

    public void TakeDamage(int damage)
    {
        Health = Math.Max(0, Health - damage);
    }
}
