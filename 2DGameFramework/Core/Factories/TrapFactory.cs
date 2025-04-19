using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;


namespace _2DGameFramework.Core.Factories
{
    internal class TrapFactory : ITrapFactory
    {
        private readonly ILogger _logger;

        public TrapFactory(ILogger logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Trap CreateTrap(string name, int damageAmount, Position position, string? description = null, bool isRemovable = false)
        {
            return new Trap(name, damageAmount, position, _logger, description, isRemovable);
        }
    }
}