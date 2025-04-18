using _2DGameFramework.Core;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Services
{
    /// <summary>
    /// Manages equipped attack and defense items for a creature.
    /// </summary>
    public class InventoryService : IInventory
    {
        private readonly List<IAttackSource> _attackItems = new();
        private readonly List<IDefenseSource> _defenseItems = new();
        private readonly List<IUsable> _usableItems = new();

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService"/> class.
        /// </summary>
        /// <param name="logger">The logger used to record inventory operations.</param>
        public InventoryService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>Adds a new attack item to the inventory.</summary>
        /// <param name="item">The attack item to equip.</param>
        public void EquipAttackItem(IAttackSource item)
        {
            _attackItems.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Equipped attack item: {((WorldObject)item).Name}");
        }

        /// <summary>Adds a new defense item to the inventory.</summary>
        /// <param name="item">The defense item to equip.</param>
        public void EquipDefenseItem(IDefenseSource item)
        {
            _defenseItems.Add(item);
            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Equipped defense item: {((WorldObject)item).Name}");
        }

        /// <summary>Calculates the total base damage from all equipped attack items.</summary>
        /// <returns>The combined hit points of all equipped attack sources.</returns>
        public int GetTotalBaseDamage() => _attackItems.Sum(item => item.BaseDamage);

        /// <summary>Calculates the total damage reduction from all equipped defense items.</summary>
        /// <returns>The combined damage reduction value of all equipped defense sources.</returns>
        public int GetTotalDamageReduction() => _defenseItems.Sum(item => item.DamageReduction);

        /// <summary>
        /// Retrieves all usable items in the inventory.
        /// </summary>
        /// <returns>A read-only collection of usable items.</returns>
        public IEnumerable<IUsable> GetUsables() => _usableItems.AsReadOnly();

        /// <summary>
        /// Processes a collection of looted items, equipping or storing them based on their type.
        /// </summary>
        /// <param name="loot">The collection of looted <see cref="WorldObject"/> items.</param>
        public void ProcessLoot(IEnumerable<WorldObject> loot)
        {
            foreach (var item in loot)
            {
                switch (item)
                {
                    case IAttackSource attackItem:
                        EquipAttackItem(attackItem);
                        break;

                    case IDefenseSource defenseItem:
                        EquipDefenseItem(defenseItem);
                        break;

                    case IUsable usableItem:
                        _usableItems.Add(usableItem);
                        _logger.Log(
                            TraceEventType.Information,
                            LogCategory.Inventory,
                            $"Stored usable item: {((WorldObject)usableItem).Name}");
                        break;

                    default:
                        _logger.Log(
                            TraceEventType.Warning,
                            LogCategory.Inventory,
                            $"Unsupported looted item type: {item.Name}");
                        break;
                }
            }
        }
    }
}
