namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Represents any named world object in the framework.
    /// </summary>
    public interface IWorldObject : IName
    {
        /// <summary>
        /// An optional description of this world object.
        /// </summary>
        string? Description { get; }
    }
}
