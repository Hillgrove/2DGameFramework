using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

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
            target.ReceiveDamage(DamageAmount);
            Console.WriteLine($"{target.Name} took {DamageAmount} HP of damage from {Name}");
        }
    }
}
