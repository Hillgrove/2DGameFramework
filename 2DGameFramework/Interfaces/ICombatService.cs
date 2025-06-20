﻿using _2DGameFramework.Domain.Creatures;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Defines combat operations such as attacking, receiving damage, and healing for creatures.
    /// </summary>
    public interface ICombatService
    {
        /// <summary>
        /// Performs an attack from <paramref name="attacker"/> to <paramref name="target"/>.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="target">The creature being attacked.</param>
        void Attack(ICreature attacker, ICreature target);

        /// <summary>
        /// Applies incoming damage to the specified creature.
        /// </summary>
        /// <param name="creature">The creature receiving damage.</param>
        /// <param name="damage">The raw damage amount to apply.</param>
        void ReceiveDamage(ICreature creature, int damage);

        /// <summary>
        /// Restores a specified amount of hit points to the creature.
        /// </summary>
        /// <param name="creature">The creature to heal.</param>
        /// <param name="amount">The amount of hit points to restore.</param>
        void Heal(ICreature creature, int amount);

        /// <summary>
        /// Performs an attack using a specific damage source.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="target">The creature being attacked.</param>
        /// <param name="source">The specific damage source to use.</param>
        void AttackWithSource(ICreature attacker, ICreature target, IDamageSource source);
    }   // TODO: why 2 attackoptions?
}
