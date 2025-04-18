using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Interfaces
{
    /// <summary>
    /// Represents any entity or container that can provide loot items.
    /// </summary>
    public interface ILootSource
    {
        /// <summary>
        /// Retrieves the collection of items available to be looted.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{ItemBase}"/> of lootable items.</returns>
        IEnumerable<ItemBase> GetLoot();
    }
}
