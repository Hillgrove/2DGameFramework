using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Models
{
    public class HealingPotion : WorldObject, IUsable
    {
        public int HealAmount { get; }

        public HealingPotion(string name, int healAmount, string? description = null) 
            : base(name, description, position: null, isLootable: false, isRemovable: false)
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
