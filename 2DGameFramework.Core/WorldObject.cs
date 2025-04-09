namespace _2DGameFramework.Core
{
    public class WorldObject
    {
        public required string Name { get; init; }
        public bool IsLootable { get; set; }
        public bool IsRemoveable { get; set; }
    }
}
