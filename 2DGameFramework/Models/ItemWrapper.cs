using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class ItemWrapper : EnvironmentObject, ILootSource
    {
        private readonly ItemBase _itemInside;
        private readonly ILogger _logger;

        public ItemWrapper(ItemBase item, Position position, ILogger logger)
            : base(item.Name, item.Description, position, isLootable: true, isRemovable: true)
        {
            _itemInside = item;
            _logger = logger;
        }

        public IEnumerable<ItemBase> GetLoot()
        {
            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"Item '{_itemInside.Name}' looted from wrapper at {Position}");
            
            return new[] { _itemInside };
        }

        public override string ToString() =>
            $"{base.ToString()} [Contains: {_itemInside.Name}]";

    }
}
