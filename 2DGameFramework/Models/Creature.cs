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

        public void Loot(ItemBase obj, World world)
        {
            if (!obj.IsLootable)
            {
                Console.WriteLine($"{obj.Name} can't be looted...");
                return;
            }

            obj.Position = null; // remove from world space
            // TODO: remove item from world - creature not currently aware of world
            // world.RemoveObject(obj); // remove 

            if (obj is WeaponBase ai) _attackItems.Add(ai);
            else if (obj is ArmorBase di) _defenseItems.Add(di);


        }

        public void UseItem(ItemBase item)
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
