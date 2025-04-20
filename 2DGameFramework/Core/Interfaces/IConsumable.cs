namespace _2DGameFramework.Core.Interfaces
{
    /// <summary>
    /// Extended usable interface to expose its category flags.
    /// </summary>
    public interface IConsumable : IUsable
    {
        ConsumableType Type { get; }
    }

    /// <summary>
    /// Categories of effects a consumable can have; combined via bit‑flags.
    /// </summary>
    [Flags]
    public enum ConsumableType
    {
        None        = 0,
        Healing     = 1 << 0,
        Damage      = 1 << 1,
        Buff        = 1 << 2,
        Debuff      = 1 << 3
    }
}

