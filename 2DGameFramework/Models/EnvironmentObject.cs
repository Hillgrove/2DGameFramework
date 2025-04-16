using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class EnvironmentObject : WorldObject
    {
        public Position Position { get; internal set; }
        public bool IsLootable { get; internal set; }
        public bool IsRemovable { get; internal set; }

        public EnvironmentObject(string name, string? description, Position position, bool isLootable = false, bool isRemovable = false)
            : base(name, description)
        {
            Position = position;
            IsLootable = isLootable;
            IsRemovable = isRemovable;
        }

        public override string ToString()
        {
            var flags = $"{(IsLootable ? "[Lootable] " : "")}{(IsRemovable ? "[Removable]" : "")}".Trim();
            return $"{base.ToString()} at {Position}" + (flags.Length > 0 ? $" {flags}" : "");
        }
    }
}
