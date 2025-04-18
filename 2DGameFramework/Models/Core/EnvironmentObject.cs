using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models.Core
{
    /// <summary>
    /// Represents an object placed in the world, with position and optional loot/removal capabilities.
    /// </summary>
    public class EnvironmentObject : WorldObject, IPositionable
    {
        public Position Position { get; internal set; }
        public bool IsLootable { get; internal set; }
        public bool IsRemovable { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentObject"/> class.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">An optional description of the object.</param>
        /// <param name="position">The initial position of the object in the world.</param>
        /// <param name="isLootable">Whether creatures can loot this object.</param>
        /// <param name="isRemovable">Whether this object can be removed from the world.</param>
        public EnvironmentObject(string name, string? description, Position position, bool isLootable = false, bool isRemovable = false)
            : base(name, description)
        {
            Position = position;
            IsLootable = isLootable;
            IsRemovable = isRemovable;
        }

        /// <summary>
        /// Returns a formatted string describing this object's base information,
        /// including its name, position, and any loot/removal flags.
        /// </summary>
        /// <returns>A string representation of this environment object.</returns>
        public override string ToString()
        {
            var flags = $"{(IsLootable ? "[Lootable] " : "")}{(IsRemovable ? "[Removable]" : "")}".Trim();
            return $"{base.ToString()} at {Position}" + (flags.Length > 0 ? $" {flags}" : "");
        }
    }
}
