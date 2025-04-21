using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Combat
{
    /// <summary>
    /// Represents a single attack action a creature can perform.
    /// </summary>
    public interface IAttackAction
    {
        /// <param name="attacker">The creature performing the action.</param>
        /// <param name="target">The creature being attacked.</param>
        void Execute(ICreature attacker, ICreature target);
    }
}
