using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class Creature
    {
        public string Name { get; }
        public int Hitpoints { get; private set; }
        public Position Position { get; private set; }

        private readonly int _maxhitpoints;
        private readonly List<WeaponBase> _attackItems = new();
        private readonly List<ArmorBase> _defenseItems = new();

        public Creature(string name, int hitPoints, Position startPosition)
        {
            Name = name;
            Hitpoints = hitPoints;
            _maxhitpoints = hitPoints;
            Position = startPosition;
        }


        public int Hit()
        {
            return _attackItems.Sum(i => i.HitDamage);

        }

        public void ReceiveDamage(int hitdamage)
        {
            int damageReduction = _defenseItems.Sum(i => i.DamageReduction);
            Hitpoints -= Math.Max(0, hitdamage - damageReduction);

            if (Hitpoints <= 0)
            {
                Console.WriteLine($"{Name} is dead...");
            }
        }

        public void Heal(int amount)
        {
            Hitpoints = Math.Min(Hitpoints + amount, _maxhitpoints);
        }

        public void Loot(ILootSource source)
        {
            if (source is not WorldObject obj || obj.IsLootable)
            {
                Console.WriteLine($"{source} can't be looted...");
                return;
            }

            foreach (var item in source.GetLoot())
            {
                if (item is WeaponBase ai) _attackItems.Add(ai);
                else if (item is ArmorBase di) _defenseItems.Add(di);
                else continue; // skip items that are not WeaponBase or ArmorBase
            }
        }

        public void UseItem(WorldObject item)
        {
            if (item is IUsable usable)
            {
                usable.UseOn(this);
            }

            else
            {
                Console.WriteLine($"{item.Name} cannot be used.");
            }
        }

        public void MoveBy(int dx, int dy)
        {
            Position = Position with { X = Position.X + dx, Y = Position.Y + dy };
        }
    }
}
