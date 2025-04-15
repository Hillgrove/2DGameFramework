using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class Container : EnvironmentObject, ILootSource
    {
        private readonly List<ItemBase> _items = new();

        public Container(string name, string? description, Position position, bool isLootable = true, bool isRemovable = false) 
            : base(name, description, position, isLootable, isRemovable)
        {
        }

        public void AddItem(ItemBase item) => _items.Add(item);
        
        public IEnumerable<ItemBase> GetLoot()
        {
            var loot = _items.ToList();
            _items.Clear();
            return loot;
        }
    }
}
