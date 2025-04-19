using _2DGameFramework.Core.Objects;

namespace _2DGameFramework.Core.Factories
{
    /// <summary>
    /// Factory for creating various types of weapons, including swords and bows.
    /// </summary>
    internal class WeaponFactory : IWeaponFactory
    {
        ///<inheritdoc/>
        public Sword CreateSword(string name, string description, int hitdamage, int range)
        {
            return new Sword(name, description, hitdamage, range);
        }

        ///<inheritdoc/>
        public Bow CreateBow(string name, string description, int hitdamage, int range)
        {
            return new Bow(name, description, hitdamage, range);
        }
    }
}
