using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Models
{
    public class ContainerBase : WorldObject, ILootSource
    {
        private readonly List<WorldObject> _items = new();

        public ContainerBase(string name, string? description, Position? position) 
            : base(name, description, position, isLootable: true, isRemovable: false)
        {
        }

        public void AddItem(WorldObject item) => _items.Add(item);
        public IEnumerable<WorldObject> GetLoot()
        {
            var loot = _items.ToList();
            _items.Clear();
            return loot;
        }
    }
}
