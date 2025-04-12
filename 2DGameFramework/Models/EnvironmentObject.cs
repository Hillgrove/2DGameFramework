using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
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
