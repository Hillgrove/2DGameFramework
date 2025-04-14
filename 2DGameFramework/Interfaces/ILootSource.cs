using _2DGameFramework.Models;

namespace _2DGameFramework.Interfaces
{
    public interface ILootSource
    {
        IEnumerable<WorldObject> GetLoot();
    }
}
