using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Indicates an object that has a 2D position in the world.
    /// </summary>
    public interface IPositionable
    {
        /// <summary>
        /// The current coordinates of this entity.
        /// </summary>
        Position Position { get; }
    }
}