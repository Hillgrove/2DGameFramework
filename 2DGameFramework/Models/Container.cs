using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class Container : EnvironmentObject, ILootSource
    {
        private readonly List<ItemBase> _items = new();
        private readonly ILogger _logger;

        public Container(string name, string? description, Position position, ILogger logger, bool isLootable = true, bool isRemovable = false) 
            : base(name, description, position, isLootable, isRemovable)
        {
            _logger = logger;
        }

        public void AddItem(ItemBase item)
        {
            _items.Add(item);

            _logger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"Item '{item.Name}' added to container '{Name}' at {Position}");
        }
        
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

        public override string ToString()
        {
            var itemList = _items.Count > 0
                ? string.Join(", ", _items.Select(i => i.Name))
                : "None";

            return $"{base.ToString()} [Items: {_items.Count}] [{itemList}]";
        }


    }
}
