using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Wraps a single item in the world as a lootable and removable object.
    /// </summary>
    public class ItemWrapper : EnvironmentObject, ILootSource
    {
        public bool IsLootable { get; internal set; }

        private readonly IItem _wrappedItem;        
        private readonly ILogger _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ItemWrapper"/> class,
        /// containing the specified item at the given position.
        /// </summary>
        /// <param name="item">The item to wrap.</param>
        /// <param name="position">The position of the wrapper in the world.</param>
        /// <param name="logger">The logger to record loot events.</param>
        public ItemWrapper(IItem wrappedItem, Position position, ILogger logger, bool isLootable = true)
            : base(wrappedItem.Name, wrappedItem.Description, position, isRemovable: true)
        {
            IsLootable = isLootable;
            _wrappedItem = wrappedItem;
            _logger = logger;
        }

        ///<inheritdoc/>
        public IEnumerable<IItem> GetLoot()
        {
            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Item '{_wrappedItem.Name}' looted from wrapper at {Position}");

            return new[] { _wrappedItem };
        }

        /// <summary>
        /// Returns a formatted string describing this wrapper,
        /// including the name of the contained item.
        /// </summary>
        /// <returns>A string representation of the item wrapper.</returns>
        public override string ToString() =>
            $"{base.ToString()} [Contains: {_wrappedItem.Name}]";

    }
}
