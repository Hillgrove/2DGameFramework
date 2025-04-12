namespace _2DGameFramework.Objects.Base
{
    public abstract class ItemBase : WorldObject
    {
        protected ItemBase(string name, string? description = null, bool isLootable = true, Position? position = null) 
            : base(name, position, description)
        {
            IsLootable = isLootable;
        }

        public bool IsLootable { get; internal set; }
    }
}
