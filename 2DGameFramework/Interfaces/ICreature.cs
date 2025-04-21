using _2DGameFramework.Domain.Creatures;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Services;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Represents a creature in the world capable of movement, combat, and item interaction.
    /// </summary>
    public interface ICreature : ICombatStats, IPositionable
    {
        string Name { get; }
        int HitPoints { get; }
        int MaxHitPoints { get; }

        event EventHandler<HealthChangedEventArgs>? HealthChanged;
        event EventHandler<DeathEventArgs>? OnDeath;

        /// <summary>Moves the creature by the specified deltas within the world bounds.</summary>
        /// <param name="deltaX">Change in X direction.</param>
        /// <param name="deltaY">Change in Y direction.</param>
        /// <param name="world">World providing boundary constraints.</param>
        void MoveBy(int deltaX, int deltaY, GameWorld world);

        /// <summary>
        /// Performs an attack on the target creature.
        /// </summary>
        /// <param name="target">The creature to attack.</param>
        void Attack(ICreature target);

        void AdjustHitPoints(int delta);
        IEnumerable<IConsumable> GetUsables();
    }
}
