﻿namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Defines an object that can be used on a creature.
    /// </summary>
    public interface IUsable : IItem
    {
        /// <summary>
        /// Applies this usable object's effect to the specified creature.
        /// </summary>
        /// <param name="target">The creature on which this object is used.</param>
        void UseOn(ICreature target);
    }
}
