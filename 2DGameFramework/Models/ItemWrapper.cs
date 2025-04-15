using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class ItemWrapper : EnvironmentObject, ILootSource
    {
        private readonly ItemBase _itemInside;

        public ItemWrapper(ItemBase item, Position position)
            : base(item.Name, item.Description, position, isLootable: true, isRemovable: true)
        {
            _itemInside = item;
        }

        public IEnumerable<ItemBase> GetLoot() => new[] { _itemInside };
    }
}
