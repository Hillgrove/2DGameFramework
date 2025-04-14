using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Interfaces
{
    public interface ILootSource
    {
        IEnumerable<WorldObject> GetLoot();
    }
}
