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
        public FastEnemy(Map map, int initialHealth, int startX, int startY) : base(map, initialHealth, startX, startY) // Create fast enemy entity using base class enemy
        {
        }

        public override void MoveRandomly(Player player, HUD hud)
        {
            for (int i = 0; i < 2; i++) // Use base enemy movement but make it move 2 spaces each time
            {
                base.MoveRandomly(player, hud);
                if (Position.x == player.Position.x && Position.y == player.Position.y) // Check if it has moved into the player's position
                {
                    player.TakeDamage(1); // Attack the player
                    hud.SetActionMessage("The fast enemy attacked you for 1 damage!");
                    break; // Don't move when attacking
                }
            }
        }
    }
}
