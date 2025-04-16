using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    public class Trap : EnvironmentObject, ITriggerable
    {
        public Trap(string name, string? description, int damageAmount, Position position, bool isLootable = false, bool isRemovable = false) 
            : base(name, description, position, isLootable, isRemovable)
        {
            DamageAmount = damageAmount;
        }

        public int DamageAmount { get; }
    
        public void ReactTo(Creature target)
        {
            GameLogger.Log(
                TraceEventType.Warning,
                LogCategory.World,
                $"{target.Name} triggered trap '{Name}' at {Position} dealing {DamageAmount} HP damage");
            
            target.ReceiveDamage(DamageAmount);
        }
    }
}
