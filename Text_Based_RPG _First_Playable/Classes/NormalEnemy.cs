using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class NormalEnemy : Enemy
    {
        public NormalEnemy(Map map, int initialHealth, int startX, int startY, int damage) : base(map, initialHealth, startX, startY, damage)
        {
        }

        public override void Attack(Player player, HUD hud)
        {
            base.Attack(player, hud);
            hud.SetActionMessage($"You took {damage} damage from a normal enemy");
        }
    }
}
