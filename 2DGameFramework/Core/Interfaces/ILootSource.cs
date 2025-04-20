namespace _2DGameFramework.Core.Interfaces
{
    /// <summary>
    /// Represents any entity or container that can provide loot items.
    /// </summary>
    public interface ILootSource : IWorldObject
    {
        bool IsLootable { get; }

        /// <summary>
        /// Retrieves the collection of items available to be looted.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{IItem}"/> of lootable items.</returns>
        IEnumerable<IItem> GetLoot();
    }
}
