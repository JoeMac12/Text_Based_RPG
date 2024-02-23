using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class FastEnemy : Enemy
    {
        public FastEnemy(Map map, int initialHealth, int startX, int startY, int damage) : base(map, initialHealth, startX, startY, damage) // Initialize fast enemy
        {
        }

        public override void MoveRandomly(Player player, HUD hud) // Use base enemy movement
        {
            for (int i = 0; i < 2; i++) // Move twice in one turn
            {
                base.MoveRandomly(player, hud); 
                if (Position.x == player.Position.x && Position.y == player.Position.y)
                {
                    Attack(player);
                    hud.SetActionMessage("The fast enemy attacked you for 1 damage!");
                    break;
                }
            }
        }
    }
}
