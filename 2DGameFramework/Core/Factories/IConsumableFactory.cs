using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Objects;

namespace _2DGameFramework.Core.Factories
{
    public interface IConsumableFactory
    {
        Consumable CreateConsumable(string name, string description, Action<Creature> effect);
    }
}
