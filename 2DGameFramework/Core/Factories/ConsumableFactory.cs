using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;

namespace _2DGameFramework.Core.Factories
{
    internal class ConsumableFactory : IConsumableFactory
    {
        private readonly ILogger _logger;

        public ConsumableFactory(ILogger logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Consumable CreateConsumable(string name, string description, Action<Creature> effect)
        {
            return new Consumable(name, effect, description, _logger);
        }
    }
}
