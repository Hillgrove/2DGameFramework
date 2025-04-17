using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    /// <summary>
    /// Represents an item that can be consumed to apply a specific effect to a creature.
    /// </summary>
    public class Consumable : ItemBase, IUsable
    {
        private readonly Action<Creature> _effect;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Consumable"/> class.
        /// </summary>
        /// <param name="name">The name of the consumable item.</param>
        /// <param name="effect">The action to perform on the target creature when used.</param>
        /// <param name="description">An optional description of the consumable.</param>
        /// <param name="logger">The logger to record usage events.</param>
        public Consumable(string name, Action<Creature> effect, string? description, ILogger logger) 
            : base(name, description)
        {
            _effect = effect;
            _logger = logger;
        }

        /// <summary>
        /// Uses this consumable on the specified creature, logging the action and executing its effect.
        /// </summary>
        /// <param name="target">The creature to apply the consumable to.</param>
        public void UseOn(Creature target)
        {
            _logger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"{Name} used on {target.Name}");

            _effect(target);
        }

        /// <summary>
        /// Returns a string representation of this consumable, including its base information.
        /// </summary>
        public override string ToString() =>
            $"{base.ToString()} [Consumable]";

    }
}
