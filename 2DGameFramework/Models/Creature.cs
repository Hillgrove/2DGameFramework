using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class Creature : WorldObject, IPositionable
    {
        public int Hitpoints { get; private set; }
        public Position Position { get; private set; }

        private readonly int _maxhitpoints;
        private readonly List<WeaponBase> _attackItems = new();
        private readonly List<ArmorBase> _defenseItems = new();
        private readonly List<IUsable> _usables = new();

        public Creature(string name, string? description, int hitpoints, Position startPosition) 
            : base(name, description)
        {
            Hitpoints = hitpoints;
            _maxhitpoints = hitpoints;
            Position = startPosition;
        }

        public IEnumerable<IUsable> GetUsables() => _usables;
        
        public void Attack(Creature target)
        {
            int damage = TotalDamage();

            GameLogger.Log(
                TraceEventType.Information,
                LogCategory.Combat,
                $"{Name} is attacking {target.Name} with total damage {damage}"
            );

            target.ReceiveDamage(damage);
        }

        public void ReceiveDamage(int hitdamage)
        {
            int damageReduction = _defenseItems.Sum(i => i.DamageReduction);
            int actualDamage = Math.Max(0, hitdamage - damageReduction);
            Hitpoints -= actualDamage;

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Combat, 
                $"{Name} received {actualDamage} damage after {damageReduction} reduction. HP now {Hitpoints}");

            if (Hitpoints <= 0)
            {
                GameLogger.Log(TraceEventType.Critical, LogCategory.Combat, $"{Name} has died.");
            }
        }

        public void Heal(int amount)
        {
            int before = Hitpoints;
            Hitpoints = Math.Min(Hitpoints + amount, _maxhitpoints);
            int actualHealed = Hitpoints - before;
            
            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Combat, 
                $"{Name} healed for {actualHealed}. HP now {Hitpoints}");
        }

        public void Loot(ILootSource source, World world)
        {
            if (source is not EnvironmentObject container || source is not (Container or ItemWrapper))
            {
                GameLogger.Log(
                    TraceEventType.Warning, 
                    LogCategory.Inventory, 
                    $"{Name} attempted to loot an invalid source.");
                
                return;
            }

            if (!container.IsLootable)
            {
                GameLogger.Log(
                    TraceEventType.Information, 
                    LogCategory.Inventory,
                    $"{Name} attempted to loot '{container.Name}', but it is currently not lootable.");

                return;
            }

            var loot = source.GetLoot();
            EquipLoot(loot);

            if (container.IsRemovable)
            {
                world.RemoveObject(container);

                GameLogger.Log(
                    TraceEventType.Information, 
                    LogCategory.Inventory, 
                    $"{container.Name} removed from world after looting.");
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
                GameLogger.Log(
                    TraceEventType.Information, 
                    LogCategory.Inventory, 
                    $"{item.Name} cannot be used by {Name}.");
            }
        }

        public void MoveBy(int dx, int dy)
        {
            var from = Position;
            Position = Position with { X = Position.X + dx, Y = Position.Y + dy };

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Game, 
                $"{Name} moved from {from} to {Position}");
        }

        public override string ToString() =>
            $"{Name} at {Position} ({Hitpoints}/{_maxhitpoints} HP)";


        #region Private Functions
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
                    GameLogger.Log(
                        TraceEventType.Warning, 
                        LogCategory.Inventory, 
                        $"{Name} ignored item '{item.Name}' – unsupported item type.");
                    break;
            }
        }

        private void EquipWeapon(WeaponBase weapon)
        {
            _attackItems.Add(weapon);

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"{Name} equipped weapon: {weapon.Name}");
        }

        private void EquipArmor(ArmorBase armor)
        {
            _defenseItems.Add(armor);

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"{Name} equipped armor: {armor.Name}");
        }

        private void AddUsable(IUsable usable)
        {
            _usables.Add(usable);

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"{Name} added usable item to backpack: {((ItemBase)usable).Name}");
        }

        private int TotalDamage()
        {
            // If no weapons equipped, do 1 HP damage with “fists”
            return _attackItems.Any()
                ? _attackItems.Sum(i => i.HitDamage)
                : 1;

        }
        #endregion
    }
}
