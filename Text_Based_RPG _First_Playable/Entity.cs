using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal abstract class Entity // Base class for initializing the player and enemy
{
    public (int x, int y) Position { get; protected set; }
    public int Health { get; protected set; }
    protected Map map;

    protected Entity(Map gameMap)
    {
        map = gameMap;
    }

    public abstract void Move();
}
