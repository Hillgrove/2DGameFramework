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
        private readonly List<IUsable> _usables = new();

        public Creature(string name, int hitPoints, Position startPosition)
        {
            Name = name;
            Hitpoints = hitPoints;
            _maxhitpoints = hitPoints;
            Position = startPosition;
        }

        public void Hit(Creature target)
        {
            int damage = TotalDamage();
            target.ReceiveDamage(damage);
            Console.WriteLine($"{Name} hit {target.Name} for {damage} HP.");
        }

        public int TotalDamage()
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

        public void Loot(ILootSource source, World world)
        {
            if (source is not EnvironmentObject container || source is not (Container or ItemWrapper))
            {
                Console.WriteLine("Invalid loot source.");
                return;
            }

            if (!container.IsLootable)
            {
                Console.WriteLine($"{container.Name} is not lootable.");
                return;
            }

            var loot = source.GetLoot();
            EquipLoot(loot);

            if (container.IsRemovable)
            {
                world.RemoveObject(container);
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

        private void EquipLoot(IEnumerable<ItemBase> loot)
        {
            foreach (var item in loot)
            {
                EquipSingleItem(item);
            }
        }

        private void EquipSingleItem(ItemBase item)
        {
            switch (item)
            {
                case WeaponBase weapon:
                    EquipWeapon(weapon);
                    break;

                case ArmorBase armor:
                    EquipArmor(armor);
                    break;

                case IUsable usable:
                    AddUsable(usable);
                    break;

                default:
                    Console.WriteLine($"{item.Name} ignored – unsupported item type.");
                    break;
            }
        }

        private void EquipWeapon(WeaponBase weapon)
        {
            _attackItems.Add(weapon);
            Console.WriteLine($"{weapon.Name} equipped as weapon.");
        }

        private void EquipArmor(ArmorBase armor)
        {
            _defenseItems.Add(armor);
            Console.WriteLine($"{armor.Name} equipped as armor.");
        }

        private void AddUsable(IUsable usable)
        {
            _usables.Add(usable);
            Console.WriteLine($"{((ItemBase)usable).Name} added to backpack.");
        }
    }
}
