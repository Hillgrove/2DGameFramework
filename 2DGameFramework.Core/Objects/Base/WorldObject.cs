namespace _2DGameFramework.Objects.Base
{
    public abstract class WorldObject
    {
        public string Name { get; }
        public string? Description { get; internal set; }
        public Position? Position { get; internal set; }

        protected WorldObject(string name, Position? position = null, string? description = null)
        {
            Name = name;
            Position = position;
            Description = description;
        }
    }
}
