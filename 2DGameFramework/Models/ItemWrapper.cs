using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Models
{
    public class ItemWrapper : WorldObject, ILootSource
    {
        private readonly WorldObject _itemInside;

        public ItemWrapper(WorldObject item, Position position)
            : base(item.Name, item.Description, position, isLootable: true, isRemovable: true)
        {
            _itemInside = item;
        }

        public IEnumerable<WorldObject> GetLoot() => new[] { _itemInside };
    }
}
