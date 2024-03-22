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

        public EnemyManager(Map map, HUD hud)
        {
            this.map = map;
            this.hud = hud;
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

