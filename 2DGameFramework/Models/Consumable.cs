using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class Consumable : ItemBase, IUsable
    {
        private readonly Action<Creature> _effect;
        private readonly ILogger _logger;

        public Consumable(string name, Action<Creature> effect, string? description, ILogger logger) 
            : base(name, description)
        {
            _effect = effect;
            _logger = logger;
        }


        public void UseOn(Creature target)
        {
            _logger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"{Name} used on {target.Name}");

            _effect(target);
        }

        public override string ToString() =>
            $"{base.ToString()} [Consumable]";

    }
}
