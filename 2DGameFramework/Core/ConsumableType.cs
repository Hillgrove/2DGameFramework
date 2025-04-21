namespace _2DGameFramework.Core
{
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
