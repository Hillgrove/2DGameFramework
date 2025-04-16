using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class Consumable : ItemBase, IUsable
    {
        private readonly Action<Creature> _effect;

        public Consumable(string name, Action<Creature> effect, string? description) : base(name, description)
        {
            _effect = effect;
        }


        public void UseOn(Creature target)
        {          
            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.Inventory, 
                $"{Name} used on {target.Name}");

            _effect(target);
        }

        public override string ToString() =>
            $"{base.ToString()} [Consumable]";

    }
}
