using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class HealingPotion : ItemBase, IUsable
    {
        public int HealAmount { get; }

        public HealingPotion(string name, int healAmount, string? description = null, bool isLootable = true, Position? position = null)
            : base(name, description, isLootable, position)
        {
            HealAmount = healAmount;
        }

        public void UseOn(Creature target)
        {
            target.Heal(HealAmount);
            Console.WriteLine($"{target.Name} healed for {HealAmount} HP using {Name}");
        }
    }
}
