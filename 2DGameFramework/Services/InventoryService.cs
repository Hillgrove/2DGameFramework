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
        private readonly List<IDamageSource> _attackItems = new();
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

        /// <inheritdoc/>
        public void EquipAttackItem(IDamageSource item)
        {
            _attackItems.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Equipped attack item: {((WorldObject)item).Name}");
        }

        /// <inheritdoc/>
        public void EquipDefenseItem(IDefenseSource item)
        {
            _defenseItems.Add(item);
            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Equipped defense item: {((WorldObject)item).Name}");
        }

        /// <inheritdoc/>
        public int GetTotalBaseDamage() => _attackItems.Sum(item => item.BaseDamage);

        /// <inheritdoc/>
        public int GetTotalDamageReduction() => _defenseItems.Sum(item => item.DamageReduction);

        /// <inheritdoc/>
        public IEnumerable<IUsable> GetUsables() => _usableItems.AsReadOnly();

        /// <inheritdoc/>
        public void ProcessLoot(IEnumerable<WorldObject> loot)
        {
            foreach (var item in loot)
            {
                switch (item)
                {
                    case IDamageSource attackItem:
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
