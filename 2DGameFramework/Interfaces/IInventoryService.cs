using _2DGameFramework.Domain.World;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Manages the creature's equipped items and inventory operations.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Equips an attack item to the creature's inventory.
        /// </summary>
        /// <param name="attackItem">The attack item to equip.</param>
        void EquipAttackItem(IDamageSource attackItem);

        /// <summary>
        /// Equips a defense item to the creature's inventory.
        /// </summary>
        /// <param name="defenseItem">The defense item to equip.</param>
        void EquipDefenseItem(IDefenseSource defenseItem);

        /// <summary>
        /// Processes loot by adding it to the appropriate inventory list (attack, defense, usable).
        /// </summary>
        /// <param name="loot">A collection of <see cref="WorldObject"/> to be processed.</param>
        void ProcessLoot(IEnumerable<IItem> loot);

        /// <summary>
        /// Gets a read-only collection of usable items in the inventory.
        /// </summary>
        /// <returns>A collection of <see cref="IConsumable"/> items.</returns>
        IEnumerable<IConsumable> GetUsables();

        IEnumerable<IDamageSource> GetAttackItems();

        IEnumerable<IDefenseSource> GetDefenseItems();

        void Loot(ICreature looter, ILootSource source, GameWorld world);

        void UseItem(ICreature user, IConsumable item);

        /// <summary>
        /// Empties the given creature’s inventory and returns all items it was carrying.
        /// </summary>
        IEnumerable<IItem> RemoveAllItems(ICreature creature);
    }
}
