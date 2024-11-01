using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Text_Based_RPG__First_Playable.Classes // Define everything
{
    class Settings
    {
        public int PlayerStartingHealth { get; set; }
        public int PlayerStartingShield { get; set; }
        public int PlayerStartingX { get; set; }
        public int PlayerStartingY { get; set; }
        public int PlayerDamage { get; set; }

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

        private const string Game_Settings = "game_settings.json";

        public Settings()
        {
            LoadDefaultSettings();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(Game_Settings))
                {
                    string jsonString = File.ReadAllText(Game_Settings);
                    var jsonSettings = JsonSerializer.Deserialize<SettingsData>(jsonString);

                    // Copy settings
                    ApplySettings(jsonSettings);
                }
            }
            catch
            {
            }
        }

        private void ApplySettings(SettingsData settings)
        {
            // Default settings
            PlayerStartingHealth = settings.PlayerStartingHealth;
            PlayerStartingShield = settings.PlayerStartingShield;
            PlayerStartingX = settings.PlayerStartingX;
            PlayerStartingY = settings.PlayerStartingY;
            PlayerDamage = settings.PlayerDamage;

            HealthHealAmount = settings.HealthHealAmount;
            ShieldRegenAmount = settings.ShieldRegenAmount;

            NormalEnemyStartingHealth = settings.NormalEnemyStartingHealth;
            NormalEnemyDamage = settings.NormalEnemyDamage;

            FastEnemyStartingHealth = settings.FastEnemyStartingHealth;
            FastEnemyDamage = settings.FastEnemyDamage;

            StraightLineEnemyStartingHealth = settings.StraightLineEnemyStartingHealth;
            StraightLineEnemyDamage = settings.StraightLineEnemyDamage;

            AcidDmg = settings.AcidDmg;
            SpikeDmg = settings.SpikeDmg;
        }

        private void LoadDefaultSettings()
        {
            // Player
            PlayerStartingHealth = 20;
            PlayerStartingShield = 10;
            PlayerStartingX = 1;
            PlayerStartingY = 1;
            PlayerDamage = 2;

            // Items
            HealthHealAmount = 5;
            ShieldRegenAmount = 5;

            // Normal Enemy
            NormalEnemyStartingHealth = 10;
            NormalEnemyDamage = 2;

            // Fast Enemy
            FastEnemyStartingHealth = 6;
            FastEnemyDamage = 1;

            // Bouncing Enemy
            StraightLineEnemyStartingHealth = 4;
            StraightLineEnemyDamage = 2;

            // World
            AcidDmg = 1;
            SpikeDmg = 8;
        }
    }

    // Settings data
    class SettingsData
    {
        public int PlayerStartingHealth { get; set; }
        public int PlayerStartingShield { get; set; }
        public int PlayerStartingX { get; set; }
        public int PlayerStartingY { get; set; }
        public int PlayerDamage { get; set; }

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
    }
}
