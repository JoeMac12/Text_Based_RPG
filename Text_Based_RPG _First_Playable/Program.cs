using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Based_RPG__First_Playable.Classes;

// TextBased RPG (Beta)
// Made by Joseph
// Last updated March 22th, 2024

namespace Text_Based_RPG__First_Playable
{
    internal class Program
    {
        static void Main(string[] args) // Start the game
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Support UTF-8 Characters

            GameManager gameManager = new GameManager();
            gameManager.StartGameLoop();
        }
    }
}
