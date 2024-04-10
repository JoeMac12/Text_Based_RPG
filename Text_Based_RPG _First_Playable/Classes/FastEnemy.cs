using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class FastEnemy : Enemy
    {
        public override string EnemyType => "Fast";

        public FastEnemy(Map map, int initialHealth, int startX, int startY, int damage) : base(map, initialHealth, startX, startY, damage) // Initialize fast enemy
        {
        }

        public override void MoveRandomly(Player player, HUD hud) // Use base enemy movement but modify it
        {
            for (int moveCount = 0; moveCount < 2; moveCount++)
            {
                base.MoveRandomly(player, hud);

                if (Position == player.Position) // Attack Player
                {
                    Attack(player, hud);
                    hud.SetActionMessage($"You took {damage} damage from fast enemy!");
                    break;
                }
            }
        }
    }
}
