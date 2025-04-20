using _2DGameFramework.Core;
using _2DGameFramework.Domain.Creatures;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Observers
{
    /// <summary>
    /// Listens for low‑HP on creatures and auto‑uses a healing item once below threshold.
    /// </summary>
    public class HealthObserver
    {
        private readonly ILogger _logger;
        private readonly IInventoryService _inventory;
        private readonly double _thresholdFraction;

        public HealthObserver(
            ILogger logger,
            IInventoryService inventory,
            double thresholdFraction = 0.25)
        {
            _logger = logger;
            _inventory = inventory;
            _thresholdFraction = thresholdFraction;
        }


        /// <summary>Subscribe a single creature’s HealthChanged event.</summary>
        public void Subscribe(ICreature creature)
        {
            creature.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(object? sender, HealthChangedEventArgs e)
        {
            if (sender is not ICreature creature) return;

            int thresholdHitPoints = (int)(creature.MaxHitPoints * _thresholdFraction);

            if (e.OldHp > thresholdHitPoints && e.NewHp <= thresholdHitPoints)
            {
                var healingItem = creature
                    .GetUsables()
                    .FirstOrDefault(u => (u.Type & ConsumableType.Healing) != 0);

                if (healingItem != null)
                {
                    _inventory.UseItem(creature, healingItem);

                    _logger.Log(TraceEventType.Information, LogCategory.Combat,
                        $"{creature.Name} auto‑used {healingItem.Name} at {creature.HitPoints}/{creature.MaxHitPoints} HP.");
                }
            }
        }
    }
}
