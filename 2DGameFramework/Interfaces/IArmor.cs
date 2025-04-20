using _2DGameFramework.Core;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// A piece of armor that reduces damage.
    /// </summary>
    public interface IArmor : IDefenseSource, IItem
    {
        ItemSlot ItemSlot { get; }
    }
}
