namespace _2DGameFramework.Core.Interfaces
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
