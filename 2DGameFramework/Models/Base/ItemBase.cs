namespace _2DGameFramework.Models.Base
{
    public abstract class ItemBase : WorldObject
    {
        protected ItemBase(string name, string? description) 
            : base(name, description)
        {

        }
    }
}
