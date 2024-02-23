using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class StraightLineEnemy : Enemy
    {
        private int direction;
        private Random random = new Random();

        public StraightLineEnemy(Map map, int initialHealth, int startX, int startY) : base(map, initialHealth, startX, startY)
        {
            direction = random.Next(4); // Random direction
        }

        public override void MoveRandomly(Player player, HUD hud)
        {
            int x = 0, y = 0;

            switch (direction)
            {
                case 0: y = -1; break; // Up
                case 1: x = 1; break;  // Right
                case 2: y = 1; break;  // Down
                case 3: x = -1; break; // Left
            }

            int newX = Position.x + x;
            int newY = Position.y + y;

            if (map.WithinBounds(newX, newY) && CanMove(newX, newY)) // Check if move is within bounds and not a wall
            {
                if (newX == player.Position.x && newY == player.Position.y)
                {
                    Attack(player); // Attack the player
                    hud.SetActionMessage("The straight line enemy attacked you for 1 damage!");
                }
                else
                {
                    Position = (newX, newY); // Move
                }
            }
            else
            {
                direction = random.Next(4); // Change direction if hitting a wall
                MoveRandomly(player, hud); // Move in that direction
            }
        }
    }
}

