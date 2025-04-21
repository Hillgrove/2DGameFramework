using _2DGameFramework.Core;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Extended usable interface to expose its category flags.
    /// </summary>
    public interface IConsumable : IItem, IUsable
    {
        ConsumableType Type { get; }
    }
}

