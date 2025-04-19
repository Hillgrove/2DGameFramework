namespace _2DGameFramework.Core.Interfaces
{
    /// <summary>
    /// Represents any named object in the framework.
    /// </summary>
    public interface IWorldObject
    {
        /// <summary>
        /// The unique name of this world object.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An optional description of this world object.
        /// </summary>
        string? Description { get; }
    }
}
