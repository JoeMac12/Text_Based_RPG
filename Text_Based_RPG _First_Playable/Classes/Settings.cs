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
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(Game_Settings))
                {
                    string jsonString = File.ReadAllText(Game_Settings);
                    var settings = JsonSerializer.Deserialize<GameSettingsJson>(jsonString);

                    // Class properties
                    PlayerStartingHealth = settings.player.startingHealth;
                    PlayerStartingShield = settings.player.startingShield;
                    PlayerStartingX = settings.player.startingPosition.x;
                    PlayerStartingY = settings.player.startingPosition.y;
                    PlayerDamage = settings.player.damage;

                    HealthHealAmount = settings.items.healthHealAmount;
                    ShieldRegenAmount = settings.items.shieldRegenAmount;

                    NormalEnemyStartingHealth = settings.enemies.normal.startingHealth;
                    NormalEnemyDamage = settings.enemies.normal.damage;

                    FastEnemyStartingHealth = settings.enemies.fast.startingHealth;
                    FastEnemyDamage = settings.enemies.fast.damage;

                    StraightLineEnemyStartingHealth = settings.enemies.straightLine.startingHealth;
                    StraightLineEnemyDamage = settings.enemies.straightLine.damage;

                    AcidDmg = settings.world.acidDamage;
                    SpikeDmg = settings.world.spikeDamage;
                }

                // If something breaks or no json file is used, just use default settings

                else
                {
                    Console.WriteLine("JSON file settings not found. Using default settings.");
                    LoadDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
                Console.WriteLine("Using default settings.");
                LoadDefaultSettings();
            }
        }

        private void LoadDefaultSettings()
        {
            // Default settings if no JSON

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

    // Classes for JSON
    class GameSettingsJson
    {
        public PlayerSettings player { get; set; }
        public ItemSettings items { get; set; }
        public EnemySettings enemies { get; set; }
        public WorldSettings world { get; set; }
    }

    class PlayerSettings
    {
        public int startingHealth { get; set; }
        public int startingShield { get; set; }
        public Position startingPosition { get; set; }
        public int damage { get; set; }
    }

    class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    class ItemSettings
    {
        public int healthHealAmount { get; set; }
        public int shieldRegenAmount { get; set; }
    }

    class EnemySettings
    {
        public EnemyTypeSettings normal { get; set; }
        public EnemyTypeSettings fast { get; set; }
        public EnemyTypeSettings straightLine { get; set; }
    }

    class EnemyTypeSettings
    {
        public int startingHealth { get; set; }
        public int damage { get; set; }
    }

    class WorldSettings
    {
        public int acidDamage { get; set; }
        public int spikeDamage { get; set; }
    }
}
