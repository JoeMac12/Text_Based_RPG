using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class EnemyManager // Main enemy manager
    {
        private List<Enemy> enemies = new List<Enemy>();
        private Map map;
        private HUD hud;
        private Settings settings;

        public EnemyManager(Map map, HUD hud, Settings settings)
        {
            this.map = map;
            this.hud = hud;
            this.settings = settings;

            InitializeEnemies();
        }

        private void InitializeEnemies() // Initialize enemies 
        {
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 15, 3, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 40, 4, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 35, 2, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 30, 5, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 23, 2, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 55, 1, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 63, 6, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 73, 7, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 85, 1, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 97, 3, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 107, 1, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 104, 5, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 115, 3, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 113, 16, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 114, 8, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 26, 17, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 60, 11, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 66, 18, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 95, 11, settings.NormalEnemyDamage));
            AddEnemy(new NormalEnemy(map, settings.NormalEnemyStartingHealth, 2, 18, settings.NormalEnemyDamage));

            // Fast Enemies
            AddEnemy(new FastEnemy(map, settings.FastEnemyStartingHealth, 28, 12, settings.FastEnemyDamage));
            AddEnemy(new FastEnemy(map, settings.FastEnemyStartingHealth, 55, 16, settings.FastEnemyDamage));
            AddEnemy(new FastEnemy(map, settings.FastEnemyStartingHealth, 90, 15, settings.FastEnemyDamage));
            AddEnemy(new FastEnemy(map, settings.FastEnemyStartingHealth, 116, 12, settings.FastEnemyDamage));
            AddEnemy(new FastEnemy(map, settings.FastEnemyStartingHealth, 15, 17, settings.FastEnemyDamage));

            // Bouncing Enemies
            AddEnemy(new StraightLineEnemy(map, settings.StraightLineEnemyStartingHealth, 70, 15, settings.StraightLineEnemyDamage));
            AddEnemy(new StraightLineEnemy(map, settings.StraightLineEnemyStartingHealth, 80, 12, settings.StraightLineEnemyDamage));
            AddEnemy(new StraightLineEnemy(map, settings.StraightLineEnemyStartingHealth, 102, 17, settings.StraightLineEnemyDamage));
            AddEnemy(new StraightLineEnemy(map, settings.StraightLineEnemyStartingHealth, 45, 17, settings.StraightLineEnemyDamage));
            AddEnemy(new StraightLineEnemy(map, settings.StraightLineEnemyStartingHealth, 4, 13, settings.StraightLineEnemyDamage));
        }

        public void AddEnemy(Enemy enemy) // Simple add enemy using list / array method
        {
            enemies.Add(enemy);
        }

        public void UpdateAll(Player player) // Now move every one of them
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.MoveRandomly(player, hud);
            }
        }

        public IEnumerable<Enemy> Enemies => enemies;
    }
}

