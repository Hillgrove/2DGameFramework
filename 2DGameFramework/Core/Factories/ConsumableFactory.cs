using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;

namespace _2DGameFramework.Core.Factories
{
    public class ConsumableFactory : IConsumableFactory
    {
        private readonly ILogger _logger;

        public ConsumableFactory(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new consumable item with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the consumable item.</param>
        /// <param name="description">A description of the consumable's effect or use.</param>
        /// <param name="effect">The action applied to the creature when the consumable is used.</param>
        /// <returns>A new <see cref="Consumable"/> object configured with the provided parameters.</returns>
        public Consumable CreateConsumable(string name, string description, Action<Creature> effect)
        {
            return new Consumable(name, effect, description, _logger);
        }
    }
}
