using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Interfaces
{
    /// <summary>
    /// A piece of armor that reduces damage.
    /// </summary>
    public interface IArmor : IDefenseSource, IItem
    {
        ItemSlot ItemSlot { get; }
    }
}
