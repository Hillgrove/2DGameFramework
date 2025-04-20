using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Services
{

    public class CombatService : ICombatService
    {
        private readonly IDamageCalculator _damageCalculator;
        private readonly ILogger _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="CombatService"/> class.
        /// </summary>
        /// <param name="damageCalculator">Calculator for computing attack damage.</param>
        /// <param name="logger">Logger for tracing combat events.</param>
        public CombatService(IDamageCalculator damageCalculator, ILogger logger)
        {
            _damageCalculator = damageCalculator;
            _logger = logger;
        }

        /// <inheritdoc />
        public void Attack(ICreature attacker, ICreature target)
        {
            int damage = _damageCalculator.CalculateDamage(attacker, target);

            _logger.Log(TraceEventType.Information, LogCategory.Combat,
                $"{attacker.Name} is attacking {target.Name} with total damage {damage}");

            ReceiveDamage(target, damage);
        }

        /// <inheritdoc />
        public void ReceiveDamage(ICreature creature, int hitdamage)
        {
            int reduction = creature.GetTotalDamageReduction();
            int actual = Math.Max(0, hitdamage - reduction);

            creature.AdjustHitPoints(-actual);

            _logger.Log(TraceEventType.Information, LogCategory.Combat,
                $"{creature.Name} received {actual} damage after {reduction} reduction. HP now {creature.HitPoints}");

            if (creature.HitPoints <= 0)
            {
                _logger.Log(TraceEventType.Information, LogCategory.Combat,
                    $"{creature.Name} has died.");
            }
        }

        /// <inheritdoc />
        public void Heal(ICreature creature, int amount)
        {
            int before = creature.HitPoints;

            creature.AdjustHitPoints(amount);
            
            int healed = creature.HitPoints - before;

            _logger.Log(TraceEventType.Information, LogCategory.Combat,
                $"{creature.Name} healed for {healed}. HP now {creature.HitPoints}");
        }
    }
}
