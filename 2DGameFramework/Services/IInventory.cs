using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Core;
using _2DGameFramework.Combat;

namespace _2DGameFramework.Services
{
    /// <summary>
    /// Manages the creature's equipped items and inventory operations.
    /// </summary>
    public interface IInventory : ICombatStats
    {
        void EquipAttackItem(IAttackSource attackItem);
        void EquipDefenseItem(IDefenseSource defenseItem);
        void ProcessLoot(IEnumerable<WorldObject> loot);
        IEnumerable<IUsable> GetUsables();
    }
}
