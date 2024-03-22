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

        public int HealthHealAmount { get; set; }
        public int ShieldRegenAmount { get; set; }

        public int NormalEnemyStartingHealth { get; set; }
        public int NormalEnemyDamage { get; set; }

        public int FastEnemyStartingHealth { get; set; }
        public int FastEnemyDamage { get; set; }

        public int StraightLineEnemyStartingHealth { get; set; }
        public int StraightLineEnemyDamage { get; set; }

        public int AcidDmg { get; set; }
        public int SpikeDmg { get; set; }

        public Settings() // Source Editor
        {
            // Player
            PlayerStartingHealth = 20;
            PlayerStartingShield = 10;
            PlayerStartingX = 1;
            PlayerStartingY = 1;

            // Items
            HealthHealAmount = 5;
            ShieldRegenAmount = 5;

            // Normal Enemy
            NormalEnemyStartingHealth = 10;
            NormalEnemyDamage = 2;

            // Fast Enemy
            FastEnemyStartingHealth = 5;
            FastEnemyDamage = 1;

            // Bouncing Enemy
            StraightLineEnemyStartingHealth = 5;
            StraightLineEnemyDamage = 2;

            // World
            AcidDmg = 1;
            SpikeDmg = 8;
        }
    }
}

