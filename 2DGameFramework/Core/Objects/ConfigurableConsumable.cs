using _2DGameFramework.Core.Base;
using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// A data‑driven consumable (potion, scroll, etc.) that applies an effect when used.
    /// </summary>
    public class ConfigurableConsumable : ItemBase, IUsable
    {
        private readonly Action<ICreature> _effect;
        private readonly ILogger _logger;

        public ConfigurableConsumable(
            string name, 
            string description,
            Action<ICreature> effect,
            ILogger logger)
                : base(name, description)
        {
            _effect = effect ?? throw new ArgumentNullException(nameof(effect));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Executes whatever effect was provided at construction.
        /// </summary>
        public void UseOn(ICreature target)
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
