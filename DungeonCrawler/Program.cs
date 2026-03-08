using DungeonCrawler.Board;
using System;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerHealth = 100;
            int playerDamage = 15;
            World world = new World(); // 10x10 grid

            Console.WriteLine("Welcome to DungeonCrawler!");
            Console.WriteLine("You are in a dungeon. Explore and survive!");
            Console.WriteLine("Commands: Move N/E/S/W, attack, look, flee, quit");
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
                
                else if (command == "attack")
                { 
                    var room = world.GetCurrentRoom();
                    if (room.Creatures.Count == 0) 
                    { 
                        Console.WriteLine("There is nothing to attack!");
                        continue;
                    }

                    var creature = room.Creatures[0];

                    if (creature.Type == "bomb")
                    {
                        Console.WriteLine("You strike the bomb...");
                        Console.WriteLine("BOOM!!! The bomb explodes instantly!");
                    }

                    creature.Health -= playerDamage;

                    Console.WriteLine($"You attack the {creature.Name} for {playerDamage} damage!");
                    if (creature.Health <= 0) 
                    { 
                        Console.WriteLine($"You have defeated the {creature.Name}!");

                        world.RemoveCreatureFromCurrentRoom(creature);

                        if (creature.IsBoss)
                        {
                            Console.WriteLine("You defeated the dungeon boss!");
                            Console.WriteLine("YOU WIN!");
                            Environment.Exit(0);
                        }
                    }

                    foreach (var enemy in room.Creatures)
                    {
                        Console.WriteLine($"The {enemy.Name} attacks you for {enemy.Damage} damage!");
                        playerHealth -= enemy.Damage;
                    }

                    Console.WriteLine($"Player HP: {playerHealth}");

                    if (playerHealth <= 0) 
                    { 
                        Console.WriteLine("You have been defeated! Game Over.");
                        break;
                    }
                }
                else if (command == "flee")
                {
                    world.Flee();
                }

                else
                {
                    Console.WriteLine("Unknown command.");
                }
            }
        }
    }
}
