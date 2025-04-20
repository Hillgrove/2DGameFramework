using _2DGameFramework.Domain.Objects;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Interfaces;
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
        private readonly List<IConsumable> _usableItems = new();
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
        public IEnumerable<IConsumable> GetUsables() => _usableItems.AsReadOnly();

        ///<inheritdoc/>
        public void Loot(ICreature looter, ILootSource source, GameWorld world)
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

        public IEnumerable<IItem> RemoveAllItems(ICreature creature)
        {
            // collect everything
            var items = new List<IItem>();
            items.AddRange(_attackItems.Cast<IItem>());
            items.AddRange(_defenseItems.Cast<IItem>());
            items.AddRange(_usableItems.Cast<IItem>());

            // clear out
            _attackItems.Clear();
            _defenseItems.Clear();
            _usableItems.Clear();

            _logger.Log(TraceEventType.Information, LogCategory.Inventory,
                $"{creature.Name} dropped {items.Count} items on death.");

            return items;
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

                    case IConsumable usableItem:
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

        public void UseItem(ICreature user, IConsumable item)
        {
            if (item is IConsumable usable)
                usable.UseOn(user);
            else
                _logger.Log(TraceEventType.Warning, LogCategory.Inventory,
                    $"{item.Name} cannot be used by {user.Name}.");
        }
    }
}
