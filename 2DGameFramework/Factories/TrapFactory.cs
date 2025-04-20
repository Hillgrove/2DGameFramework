using _2DGameFramework.Core;
using _2DGameFramework.Domain.Objects;
using _2DGameFramework.Logging;

// TODO: maybe allow something like world.place(trap, (3,5))
namespace _2DGameFramework.Factories
{
    internal class TrapFactory : ITrapFactory
    {
        private readonly ILogger _logger;

        public TrapFactory(ILogger logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Trap CreateTrap(string name, string description, int damageAmount, DamageType damageType, Position position, bool isRemovable = false)
        {
            return new Trap(name, damageAmount, position, _logger, description, isRemovable);
        }
    }
}