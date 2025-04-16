namespace _2DGameFramework.Models.Base
{
    public abstract class WorldObject
    {
        public string Name { get; }
        public string? Description { get; }

        protected WorldObject(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString() =>
            string.IsNullOrWhiteSpace(Description) ? Name : $"{Name} ({Description})";

    }
}
