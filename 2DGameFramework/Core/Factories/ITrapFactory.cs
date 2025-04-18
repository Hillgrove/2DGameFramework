using _2DGameFramework.Core.Objects;


namespace _2DGameFramework.Core.Factories
{
    public interface ITrapFactory
    {
        Trap CreateTrap(string name, int damageAmount, Position position, string? description = null, bool isLootable = false, bool isRemovable = false);
    }
}