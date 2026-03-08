namespace DungeonCrawler.Board
{
    public class Creature
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; }
        public bool IsBoss { get; set; }


        public Creature(string name, int health, int damage, string type, bool boss)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Type = type;
            IsBoss = boss;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}
