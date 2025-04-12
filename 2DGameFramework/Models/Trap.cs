using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class Trap : ItemBase, IUsable
    {
        public int DamageAmount { get; }

        public Trap(string name, int damageAmount, string? description = null, bool isLootable = true, Position? position = null) : base(name, description, isLootable, position)
        {
            DamageAmount = damageAmount;
        }

        public void UseOn(Creature target)
        {
            target.ReceiveDamage(DamageAmount);
            Console.WriteLine($"{target.Name} took {DamageAmount} HP of damage from {Name}");
        }
    }
}
