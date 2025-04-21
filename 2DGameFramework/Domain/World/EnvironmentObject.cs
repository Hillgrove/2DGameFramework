using _2DGameFramework.Core;
using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.World
{
    /// <summary>
    /// Represents an object placed in the world, with position and optional removal capability.
    /// </summary>
    public class EnvironmentObject : WorldObject, IPositionable
    {
        public Position Position { get; internal set; }
        public bool IsRemovable { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentObject"/> class.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">An optional description of the object.</param>
        /// <param name="position">The initial position of the object in the world.</param>
        /// <param name="isRemovable">Whether this object can be removed from the world.</param>
        public EnvironmentObject(string name, string? description, Position position, bool isRemovable = false)
            : base(name, description)
        {
            Position = position;
            IsRemovable = isRemovable;
        }

        /// <summary>
        /// Returns a formatted string describing this object's base information,
        /// including its name, description, position, and removable flag.
        /// </summary>
        /// <returns>A string representation of this environment object.</returns>
        public override string ToString()
        {
            var flag = IsRemovable ? "[Removable]" : string.Empty;
            return string.IsNullOrEmpty(flag)
                ? $"{base.ToString()} at {Position}"
                : $"{base.ToString()} at {Position} {flag}";
        }
    }
}
