using _2DGameFramework.Core;
using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Services
{
    /// <summary>
    /// Manages equipped attack and defense items for a creature.
    /// </summary>
    public class InventoryService : IInventoryService
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
        public IEnumerable<IDamageSource> GetAttackItems() => _attackItems.AsReadOnly();

        /// <inheritdoc/>
        public IEnumerable<IDefenseSource> GetDefenseItems() => _defenseItems.AsReadOnly();

        /// <inheritdoc/>
        public IEnumerable<IUsable> GetUsables() => _usableItems.AsReadOnly();

        public void Loot(ICreature looter, ILootSource source, World world)
        {
            if (!source.IsLootable)
            {
                _logger.Log(TraceEventType.Information, LogCategory.Inventory,
                    $"{looter.Name} tried to loot {source.Name}, but it's not lootable right now.");
                return;
            }

            var items = source.GetLoot();
            ProcessLoot(items);

            if (source is ItemWrapper)
            {
                world.RemoveObject((EnvironmentObject)source);
                _logger.Log(TraceEventType.Information, LogCategory.Inventory,
                    $"{source.Name} removed after looting.");
            }
        }

        /// <inheritdoc/>
        public void ProcessLoot(IEnumerable<IItem> loot)
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

        public void UseItem(ICreature user, IUsable item)
        {
            if (item is IUsable usable)
                usable.UseOn(user);
            else
                _logger.Log(TraceEventType.Warning, LogCategory.Inventory,
                    $"{item.Name} cannot be used by {user.Name}.");
        }
    }
}
