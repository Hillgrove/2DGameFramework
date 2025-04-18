namespace _2DGameFramework.Core.Interfaces
{
    /// <summary>
    /// Indicates an object that has a 2D position in the world.
    /// </summary>
    public interface IPositionable
    {
        Position Position { get; }
    }
}