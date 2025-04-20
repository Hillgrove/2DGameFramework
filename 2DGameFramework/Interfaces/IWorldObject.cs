namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Represents any named world object in the framework.
    /// </summary>
    public interface IWorldObject
    {
        /// <summary>
        /// The name of this world object.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An optional description of this world object.
        /// </summary>
        string? Description { get; }
    }
}
