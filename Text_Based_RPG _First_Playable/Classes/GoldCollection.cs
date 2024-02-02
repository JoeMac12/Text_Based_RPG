using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class GoldCollection // Initialize 
{
    private Map map;
    private HUD hud;
    private int goldScore = 0;

    public GoldCollection(Map map, HUD hud) // Method that upates the map and gold counter
    {
        this.map = map;
        this.hud = hud;
    }

    public bool CheckForGold(int x, int y) // If the player has moved into gold
    {
        if (map.map[y, x] == 'Θ') // check if it's gold
        {
            map.map[y, x] = '.'; // Replace gold with background after pickup
            goldScore++;
            hud.UpdateGoldScore(goldScore);
            hud.SetActionMessage("You collected a gold coin!");
            return true;
        }
        return false;
    }

    public int GoldScore => goldScore;
}
