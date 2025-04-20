using _2DGameFramework.Core;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// A weapon that deals damage.
    /// </summary>
    public interface IWeapon : IDamageSource, IItem
    {
        int Range { get; }
        WeaponType WeaponType { get; }
    }
}
