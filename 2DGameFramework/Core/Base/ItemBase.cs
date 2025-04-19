namespace _2DGameFramework.Core.Base
{
    /// <summary>
    /// Abstract base class for all game‑world items.
    /// Inherits core identity properties (Name, Description, etc.) from <see cref="WorldObject"/>.
    /// Specialized item types—like weapons, armor, or consumables—derive from this class.
    /// </summary>
    public abstract class ItemBase : WorldObject
    {
        protected ItemBase(string name, string description)
            : base(name, description)
        {
        }
    }
}
