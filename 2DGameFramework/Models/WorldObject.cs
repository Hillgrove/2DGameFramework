namespace _2DGameFramework.Models
{
    public class WorldObject
    {
        public string Name { get; }
        public string? Description { get; internal set; }
        public Position? Position { get; internal set; }

        public bool IsLootable { get; internal set; }
        public bool IsRemovable { get; internal set; }

        protected WorldObject(string name, string? description, Position? position, bool isLootable, bool isRemovable)
        {
            Name = name;
            Position = position;
            Description = description;
            IsLootable = isLootable;
            IsRemovable = isRemovable;
        }
    }
}
