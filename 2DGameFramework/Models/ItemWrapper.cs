using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    /// <summary>
    /// Wraps a single item in the world as a lootable and removable object.
    /// </summary>
    public class ItemWrapper : EnvironmentObject, ILootSource
    {
        private readonly ItemBase _itemInside;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemWrapper"/> class,
        /// containing the specified item at the given position.
        /// </summary>
        /// <param name="item">The item to wrap.</param>
        /// <param name="position">The position of the wrapper in the world.</param>
        /// <param name="logger">The logger to record loot events.</param>
        public ItemWrapper(ItemBase item, Position position, ILogger logger)
            : base(item.Name, item.Description, position, isLootable: true, isRemovable: true)
        {
            _itemInside = item;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the wrapped item, logging the action.
        /// </summary>
        /// <returns>An enumerable containing the single wrapped item.</returns>
        public IEnumerable<ItemBase> GetLoot()
        {
            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Item '{_itemInside.Name}' looted from wrapper at {Position}");
            
            return new[] { _itemInside };
        }

        /// <summary>
        /// Returns a formatted string describing this wrapper,
        /// including the name of the contained item.
        /// </summary>
        /// <returns>A string representation of the item wrapper.</returns>
        public override string ToString() =>
            $"{base.ToString()} [Contains: {_itemInside.Name}]";

    }
}
