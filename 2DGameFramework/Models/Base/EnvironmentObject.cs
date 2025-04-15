namespace _2DGameFramework.Models.Base
{
    public abstract class EnvironmentObject : WorldObject
    {
        public Position Position { get; internal set; }
        public bool IsLootable { get; internal set; }
        public bool IsRemovable { get; internal set; }

        protected EnvironmentObject(string name, string? description, Position position, bool isLootable = false, bool isRemovable = false) 
            : base(name, description)
        {
            Position = position;
            IsLootable = isLootable;
            IsRemovable = isRemovable;
        }
    }
}
