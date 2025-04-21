namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Represents any named item in the framework.
    /// </summary>
    public interface IItem : IName
    {
        string Description { get; }
    }
}
