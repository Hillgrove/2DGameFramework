using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class Trap : EnvironmentObject, ITriggerable
    {
        private readonly ILogger _logger;

        public Trap(string name, string? description, int damageAmount, Position position, ILogger logger, bool isLootable = false, bool isRemovable = false) 
            : base(name, description, position, isLootable, isRemovable)
        {
            DamageAmount = damageAmount;
            _logger = logger;
        }

        public int DamageAmount { get; }
    
        public void ReactTo(Creature target)
        {
            _logger.Log(
                TraceEventType.Warning,
                LogCategory.World,
                $"{target.Name} triggered trap '{Name}' at {Position} dealing {DamageAmount} HP damage");
            
            target.ReceiveDamage(DamageAmount);
        }

        public override string ToString() =>
            $"{base.ToString()} [Trap: {DamageAmount} dmg]";

    }
}
