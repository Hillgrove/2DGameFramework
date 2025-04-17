using _2DGameFramework.Models;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Defines an object that can be triggered by a creature.
    /// </summary>
    public interface ITriggerable
    {
        /// <summary>
        /// Reacts to being triggered by the specified creature.
        /// </summary>
        /// <param name="target">The creature that triggered this object.</param>
        void ReactTo(Creature target);
    }
}
