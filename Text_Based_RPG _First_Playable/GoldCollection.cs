using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class GoldCollection
{
    private Map map;
    private HUD hud;
    private int goldScore = 0;

    public GoldCollection(Map map, HUD hud)
    {
        this.map = map;
        this.hud = hud;
    }

    public bool CheckForGold(int x, int y)
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
