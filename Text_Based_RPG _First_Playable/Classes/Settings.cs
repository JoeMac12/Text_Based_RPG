using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes // Define everything
{
    class Settings
    {
        public int PlayerStartingHealth { get; set; }
        public int PlayerStartingShield { get; set; }
        public int PlayerStartingX { get; set; }
        public int PlayerStartingY { get; set; }

        public int EnemyStartingHealth { get; set; }
        public int EnemyStartingX { get; set; }
        public int EnemyStartingY { get; set; }
        public int EnemyDamage { get; set; }

        public int FastEnemyStartingHealth { get; set; }
        public int FastEnemyStartingX { get; set; }
        public int FastEnemyStartingY { get; set; }
        public int FastEnemyDamage { get; set; }

        public int StraightLineEnemyStartingHealth { get; set; }
        public int StraightLineEnemyStartingX { get; set; }
        public int StraightLineEnemyStartingY { get; set; }
        public int StraightLineEnemyDamage { get; set; }

        public Settings() // Source Editor
        {
            // Player
            PlayerStartingHealth = 20;
            PlayerStartingShield = 10;
            PlayerStartingX = 1;
            PlayerStartingY = 1;

            // Basic Enemy
            EnemyStartingHealth = 10;
            EnemyStartingX = 20;
            EnemyStartingY = 15;
            EnemyDamage = 2;

            // Fast Enemy
            FastEnemyStartingHealth = 5;
            FastEnemyStartingX = 31;
            FastEnemyStartingY = 10;
            FastEnemyDamage = 1;

            // Bouncing Enemy
            StraightLineEnemyStartingHealth = 5;
            StraightLineEnemyStartingX = 70;
            StraightLineEnemyStartingY = 15;
            StraightLineEnemyDamage = 4;
        }
    }
}

