using DungeonCrawler.Board;
using System;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World(); // 10x10 grid

            Console.WriteLine("Welcome to DungeonCrawler!");
            Console.WriteLine("You are in a dungeon. Explore and survive!");
            Console.WriteLine("Commands: Move N.E.S.W., look, quit");
            Console.WriteLine("[P]\tPlayer location\r\n[I]\tRoom has item(s)\r\n[C]\tRoom has creature(s)\r\n[B]\tRoom has both item & creature\r\n[.]\tRoom has been visited and is cleared (items/creatures removed or empty)\r\n[ ]\tEmpty space");
            world.DisplayMap();
            Console.WriteLine(world.Look());

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(input)) continue;

                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                if (command == "move" && parts.Length > 1)
                {
                    world.Move(parts[1]);
                }
                else if (command == "look")
                {
                    Console.WriteLine(world.Look());
                }
                else if (command == "quit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }
            }
        }
    }
}
