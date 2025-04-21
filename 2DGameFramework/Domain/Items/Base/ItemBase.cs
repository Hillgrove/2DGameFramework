using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Items.Base
{

    public abstract class ItemBase : IItem
    {
        public string Name { get; }
        public string Description { get; }
        
        protected ItemBase(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public override string ToString() =>
            $"{Name} ({Description})";

    }
}
