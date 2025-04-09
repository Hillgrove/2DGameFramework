namespace _2DGameFramework.Core
{
    public class Creature
    {
        public required string Name { get; init; }

        private int _hitpoints;
        private List<AttackItem> _attackItems = new();
        private List<DefenseItem> _defenseItems = new();

        public Creature(string name, int hitPoints)
        {
            Name = name;
            _hitpoints = hitPoints;
        }


        public int Hit()
        {
            return _attackItems.Sum(i => i.HitDamage);

        }

        public void ReceiveHit(int hitdamage)
        {
            int damageReduction = _defenseItems.Sum(i => i.ReduceHitPoints);
            _hitpoints -= Math.Max(0, hitdamage - damageReduction);

            if (_hitpoints <= 0)
            {
                Console.WriteLine($"{Name} is dead...");
            }
        }

        public void Loot(WorldObject obj)
        {
            if (!obj.IsLootable)
            {
                Console.WriteLine($"{obj.Name} can't be looted...");
            }

            Type objType = obj.GetType();

            if (objType == typeof(AttackItem))
            {
                _attackItems.Add((AttackItem)obj);
            }

            if (objType == typeof(DefenseItem))
            {
                _defenseItems.Add((DefenseItem)obj);
            }
        }
    }
}
