using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Board
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Item> Items { get; set; }
        public List<Creature> Creatures { get; set; }

        public bool HasBeenVisited { get; set; } // NEW: marks if player has entered/looked at this room
        public bool IsCleared { get; set; } // true if no items/creatures remain

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Creatures = new List<Creature>();
            HasBeenVisited = false;
            IsCleared = Items.Count == 0 && Creatures.Count == 0; // empty rooms start cleared
        }

        public string GetFullDescription()
        {
            HasBeenVisited = true; // mark room as visited whenever looked at

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"=== {Name} ===");
            sb.AppendLine(Description);

            if (Items.Count > 0)
            {
                sb.AppendLine("Items here:");
                foreach (var item in Items)
                    sb.AppendLine($"- {item.Name}");
            }

            if (Creatures.Count > 0)
            {
                sb.AppendLine("Creatures here:");
                foreach (var creature in Creatures)
                    sb.AppendLine($"- {creature.Name}");
            }

            // Update IsCleared if no items/creatures left
            if (Items.Count == 0 && Creatures.Count == 0)
                IsCleared = true;

            return sb.ToString();
        }
    }
}
