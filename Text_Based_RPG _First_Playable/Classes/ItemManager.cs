using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

internal class ItemManager
{
    private Map map;
    private HUD hud;
    private Settings settings;
    private Random random;

    public ItemManager(Map map, HUD hud, Settings settings)
    {
        this.map = map;
        this.hud = hud;
        this.settings = settings;
        this.random = new Random();
    }

    public void SpawnItems(int numHealthItems, int numShieldItems, int numTeleportItems) // Items to spawn
    {
        SpawnItem('♥', numHealthItems); // Health
        SpawnItem('$', numShieldItems); // Shield
        SpawnItem('♦', numTeleportItems); // Teleport
    }

    private void SpawnItem(char itemSymbol, int numItems) // Spawn them on the map
    {
        for (int i = 0; i < numItems; i++)
        {
            int x, y;
            do
            {
                x = random.Next(map.mapWidth);
                y = random.Next(map.mapHeight);
            } while (!CanSpawnItem(x, y));

            map.map[y, x] = itemSymbol;
        }
    }

    private bool CanSpawnItem(int x, int y)
    {
        char tile = map.map[y, x];
        return tile == '.'; // Only spawn on empty floor tiles
    }

    public void CheckForItemPickup(Player player) // When picking up, replace tile with background
    {
        int x = player.Position.x;
        int y = player.Position.y;
        char tile = map.map[y, x];

        switch (tile)
        {
            case '♥': // Health
                player.RegenerateHealth(settings.HealthHealAmount);
                map.map[y, x] = '.';
                hud.SetActionMessage($"Your health has been increased by {settings.HealthHealAmount} HP!");
                break;
            case '$': // Shield
                player.RegenerateShield(settings.ShieldRegenAmount);
                map.map[y, x] = '.';
                hud.SetActionMessage($"Your shield has been increased by {settings.ShieldRegenAmount} HP!");
                break;
            case '♦': // Teleport
                player.TeleportRandomly();
                map.map[y, x] = '.';
                hud.SetActionMessage("You have been teleported to a random location!");
                break;
        }
    }
}

