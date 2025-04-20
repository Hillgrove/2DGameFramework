using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Core;
using _2DGameFramework.Core.Creatures;

namespace _2DGameFramework.Services
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
        /// <returns>A collection of <see cref="IUsable"/> items.</returns>
        IEnumerable<IUsable> GetUsables();

        void Loot(ICreature looter, ILootSource source, World world);
        
        void UseItem(ICreature user, IUsable item);

        IEnumerable<IDamageSource> GetAttackItems();
        
        IEnumerable<IDefenseSource> GetDefenseItems();
    }
}
