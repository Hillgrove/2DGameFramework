using _2DGameFramework.Interfaces;
using System.ComponentModel;

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
        private readonly List<IUsable> _usables = new();

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


        // TODO: currently not deleting itemwrapper if looting from it
        public void Loot(ILootSource source, World world)
        {
            if (source is not WorldObject container)
            {
                Console.WriteLine("Invalid loot source.");
                return;
            }

            if (!container.IsLootable)
            {
                Console.WriteLine($"{container.Name} is not lootable.");
            }

            foreach (var item in source.GetLoot())
            {
                switch (item)
                {
                    case WeaponBase weapon:
                        _attackItems.Add(weapon);
                        Console.WriteLine($"{weapon.Name} equipped as weapon.");
                        break;

                    case ArmorBase armor:
                        _defenseItems.Add(armor);
                        Console.WriteLine($"{armor.Name} equipped as armor.");
                        break;

                    case IUsable usable:
                        _usables.Add(usable);
                        Console.WriteLine($"{item.Name} added to backpack.");
                        break;

                    default:
                        Console.WriteLine($"{item.Name} ignored – unsupported item type.");
                        break;
                }
            }

            if (container is ItemWrapper wrapper && wrapper.IsRemovable)
            {
                world.RemoveObject(wrapper);
            }
        }

        public IEnumerable<IUsable> GetUsables() => _usables;

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
