using _2DGameFramework.Objects.Base;

namespace _2DGameFramework
{
    public class Creature
    {
        public required string Name { get; init; }
        public Position Position { get; private set; }

        private int _hitpoints;
        private readonly List<WeaponBase> _attackItems = new();
        private readonly List<ArmorBase> _defenseItems = new();

        public Creature(string name, int hitPoints, Position startPosition)
        {
            Name = name;
            _hitpoints = hitPoints;
            Position = startPosition;
        }


        public int Hit()
        {
            return _attackItems.Sum(i => i.HitDamage);

        }

        public void ReceiveHit(int hitdamage)
        {
            int damageReduction = _defenseItems.Sum(i => i.DamageReduction);
            _hitpoints -= Math.Max(0, hitdamage - damageReduction);

            if (_hitpoints <= 0)
            {
                Console.WriteLine($"{Name} is dead...");
            }
        }

        public void Loot(ItemBase obj)
        {
            if (!obj.IsLootable)
            {
                Console.WriteLine($"{obj.Name} can't be looted...");
                return;
            }

            obj.Position = null; // as item is now picked up and doesn't exist in the world space

            if (obj is WeaponBase ai) _attackItems.Add(ai);
            else if (obj is ArmorBase di) _defenseItems.Add(di);
        }

        public void MoveBy(int dx, int dy)
        {
            Position = Position with { X = Position.X + dx, Y = Position.Y + dy };
        }
    }
}
