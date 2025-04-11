namespace _2DGameFramework.Core.Objects
{
    public class WorldObject
    {
        public required string Name { get; init; }
        public string? Description { get; internal set; }
        public bool IsLootable { get; internal set; }
        public bool IsRemoveable { get; internal set; }
        public Position? Position { get; internal set; }
    }
}
