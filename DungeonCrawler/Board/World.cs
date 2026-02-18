using System;

namespace DungeonCrawler.Board
{
    public class World
    {
        private Room[,] map;
        private int playerX;
        private int playerY;
        private int width;
        private int height;

        public World(int width = 10, int height = 10)
        {
            this.width = width;
            this.height = height;

            map = new Room[width, height];
            InitializeWorld();

            playerX = 1;
            playerY = 0; // starting position
        }

        private void InitializeWorld()
        {
            // Example rooms with items and creatures
            map[0, 0] = new Room("Forest", "A quiet forest with tall trees.");
            map[1, 0] = new Room("Village", "The center of a small village.");
            map[2, 0] = new Room("Cave Entrance", "A dark cave entrance looms.");
            map[3, 0] = new Room("River", "A flowing river blocks the path.");

            map[0, 1] = new Room("Hills", "Rolling hills stretch into the distance.");
            map[1, 1] = new Room("Market", "Vendors sell their wares here.");
            map[2, 1] = new Room("Abandoned Hut", "A small, empty hut stands here.");
            map[3, 1] = new Room("Bridge", "A bridge crosses the river.");

            // Add items and creatures
            map[1, 0].Items.Add(new Item("Lantern", "An old oil lantern."));
            map[2, 0].Creatures.Add(new Creature("Goblin", 20));
            map[3, 1].Items.Add(new Item("Sword", "A rusty sword."));
            map[3, 1].Creatures.Add(new Creature("Wolf", 15));

            // Fill remaining null tiles with empty rooms
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == null)
                        map[x, y] = new Room($"Empty Room {x},{y}", "This room is empty.");
                }
            }
        }

        public bool Move(string direction)
        {
            int newX = playerX;
            int newY = playerY;

            switch (direction.ToLower())
            {
                case "n": newY--; break;
                case "s": newY++; break;
                case "e": newX++; break;
                case "w": newX--; break;
                default:
                    Console.WriteLine("Invalid direction.");
                    return false;
            }

            if (newX < 0 || newX >= width || newY < 0 || newY >= height)
            {
                Console.WriteLine("You can't go that way.");
                return false;
            }

            playerX = newX;
            playerY = newY;

            Console.WriteLine($"You move {direction}.");
            DisplayMap();
            return true;
        }

        public string Look()
        {
            return map[playerX, playerY].GetFullDescription();
        }

        public Room GetCurrentRoom()
        {
            return map[playerX, playerY];
        }

        public void RemoveItemFromCurrentRoom(Item item)
        {
            map[playerX, playerY].Items.Remove(item);
            if (map[playerX, playerY].Items.Count == 0 && map[playerX, playerY].Creatures.Count == 0)
                map[playerX, playerY].IsCleared = true;
        }

        public void RemoveCreatureFromCurrentRoom(Creature creature)
        {
            map[playerX, playerY].Creatures.Remove(creature);
            if (map[playerX, playerY].Items.Count == 0 && map[playerX, playerY].Creatures.Count == 0)
                map[playerX, playerY].IsCleared = true;
        }

        // Side-scrolling display with symbols
        public void DisplayMap()
        {
            int startX = Math.Max(playerX - 2, 0);
            int startY = Math.Max(playerY - 2, 0);
            int endX = Math.Min(playerX + 2, width - 1);
            int endY = Math.Min(playerY + 2, height - 1);

            Console.WriteLine("\nMap View:");

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // make player yellow
                        Console.Write("[P] "); // player
                        Console.ResetColor(); // reset color for other symbols
                    }
                    else if (map[x, y] != null)
                    {
                        bool hasItem = map[x, y].Items.Count > 0;
                        bool hasCreature = map[x, y].Creatures.Count > 0;

                        if (hasItem && hasCreature)
                            Console.Write("[B] "); // Both
                        else if (hasCreature)
                            Console.Write("[C] "); // Creature
                        else if (hasItem)
                            Console.Write("[I] "); // Item
                        else if (map[x, y].IsCleared && map[x, y].HasBeenVisited)
                            Console.Write("[.] "); // cleared/looted & visited
                        else
                            Console.Write("[ ] "); // Empty space
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
