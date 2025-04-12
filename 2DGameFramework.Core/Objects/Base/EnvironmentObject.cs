namespace _2DGameFramework.Objects.Base
{
    public class EnvironmentObject : WorldObject
    {
        public EnvironmentObject(string name, Position position, bool isRemovable = false, string? description = null) 
            : base(name, position, description)
        {
            IsRemovable = isRemovable;
        }

        public bool IsRemovable { get; }
    }
}
