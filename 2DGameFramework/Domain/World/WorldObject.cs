using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.World
{
    /// <summary>
    /// Base type for all objects placed in the 2D world, carrying a name and optional description.
    /// </summary>
    public abstract class WorldObject : IWorldObject
    {
        public string Name { get; }
        public string? Description { get; }

        protected WorldObject(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Returns a formatted string describing this object’s base information,
        /// including its name and optional description.
        /// </summary>
        /// <returns>A string representation of this world object.</returns>
        public override string ToString() =>
            string.IsNullOrWhiteSpace(Description) ? Name : $"{Name} ({Description})";

    }
}
