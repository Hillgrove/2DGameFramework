using _2DGameFramework.Core;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Indicates an object that has a 2D position in the world.
    /// </summary>
    public interface IPositionable
    {
        Position Position { get; }
    }
}