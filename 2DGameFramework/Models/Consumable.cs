using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

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
            _effect(target);
            Console.WriteLine($"{Name} used on {target.Name}");
        }
    }
}
