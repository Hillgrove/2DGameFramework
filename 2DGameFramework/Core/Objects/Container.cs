using _2DGameFramework.Core.Base;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a container in the world that can hold items for creatures to loot.
    /// </summary>
    public class Container : EnvironmentObject, ILootSource
    {
        private readonly List<ItemBase> _items = new();
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="name">The name of the container.</param>
        /// <param name="description">An optional description of the container.</param>
        /// <param name="position">The position of the container in the world.</param>
        /// <param name="logger">The logger to record inventory events.</param>
        /// <param name="isLootable">Whether the container can be looted.</param>
        /// <param name="isRemovable">Whether the container can be removed from the world.</param>
        public Container(string name, string? description, Position position, ILogger logger, bool isLootable = true, bool isRemovable = false)
            : base(name, description, position, isLootable, isRemovable)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds an item to this container and logs the action.
        /// </summary>
        /// <param name="item">The item to add to the container.</param>
        public void AddItem(ItemBase item)
        {
            _items.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Item '{item.Name}' added to container '{Name}' at {Position}");
        }

        /// <summary>
        /// Retrieves and removes all items from this container, logging the action.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="ItemBase"/> instances that were in the container.
        /// </returns>
        public IEnumerable<ItemBase> GetLoot()
        {
            var loot = _items.ToList();
            _items.Clear();

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Loot retrieved from container '{Name}' at {Position}. Items: {loot.Count}");

            return loot;
        }

        /// <summary>
        /// Returns a formatted string describing this container’s base information,
        /// including its name, current item count, and list of item names.
        /// </summary>
        /// <returns>A string representation of this container.</returns>
        public override string ToString()
        {
            var itemList = _items.Count > 0
                ? string.Join(", ", _items.Select(i => i.Name))
                : "None";

            return $"{base.ToString()} [Items: {_items.Count}] [{itemList}]";
        }


    }
}
