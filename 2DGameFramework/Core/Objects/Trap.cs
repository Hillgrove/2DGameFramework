using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a trap in the world that deals damage when a creature triggers it.
    /// </summary>
    public class Trap : EnvironmentObject, ITriggerable
    {
        public int DamageAmount { get; }

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trap"/> class.
        /// </summary>
        /// <param name="name">The name of the trap.</param>
        /// <param name="description">An optional description of the trap.</param>
        /// <param name="damageAmount">The amount of damage dealt when triggered.</param>
        /// <param name="position">The position of the trap in the world.</param>
        /// <param name="logger">The logger to record trap events.</param>
        /// <param name="isLootable">Whether the trap can be looted after triggering.</param>
        /// <param name="isRemovable">Whether the trap can be removed from the world.</param>
        public Trap(string name, string? description, int damageAmount, Position position, ILogger logger, bool isLootable = false, bool isRemovable = false)
            : base(name, description, position, isLootable, isRemovable)
        {
            DamageAmount = damageAmount;
            _logger = logger;
        }


        /// <inheritdoc/>
        public void ReactTo(Creature target)
        {
            _logger.Log(
                TraceEventType.Warning,
                LogCategory.World,
                $"{target.Name} triggered trap '{Name}' at {Position} dealing {DamageAmount} HP damage");

            target.ReceiveDamage(DamageAmount);
        }

        /// <inheritdoc/>
        public override string ToString() =>
            $"{base.ToString()} [Trap: {DamageAmount} dmg]";

    }
}
