using _2DGameFramework.Core;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Domain.Objects
{
    /// <summary>
    /// Represents a container in the world that can hold items for creatures to loot.
    /// </summary>
    public class Container : EnvironmentObject, ILootSource
    {
        public bool IsLootable { get; internal set; }

        private readonly List<IItem> _items = new();
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
        public Container(string name, string description, Position position, ILogger logger, bool isLootable = true, bool isRemovable = false)
            : base(name, description, position, isRemovable)
        {
            IsLootable = isLootable;
            _logger = logger;
        }

        /// <summary>
        /// Adds an item to this container and logs the action.
        /// </summary>
        /// <param name="item">The item to add to the container.</param>
        public void AddItem(IItem item)
        {
            _items.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Item '{item.Name}' added to container '{Name}' at {Position}");
        }

        ///<inheritdoc/>
        public IEnumerable<IItem> GetLoot(ICreature looter)
        {
            var loot = _items.ToList();
            _items.Clear();

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"{looter.Name} looted {loot.Count} items from container '{Name}' at {Position}.");

            return loot;
        }

        /// <summary>
        /// Overrides ToString to include current contents
        /// </summary>
        public override string ToString()
        {
            var contents = _items.Any()
                ? string.Join(", ", _items.Select(i => i.Name))
                : "(empty)";

            return $"{Name} at {Position} contains: [{contents}]";
        }

        /// <summary>
        /// Returns items currently in the container without removing them.
        /// </summary>
        public IEnumerable<IItem> PeekLoot() => _items.AsReadOnly();




    }
}
