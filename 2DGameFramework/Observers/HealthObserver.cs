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
        private readonly Dictionary<ICreature, double> _thresholds = new();

        public HealthObserver(
            ILogger logger,
            IInventoryService inventory)
        {
            _logger = logger;
            _inventory = inventory;
        }


        /// <summary>Subscribe a single creature’s HealthChanged event.</summary>
        public void Subscribe(ICreature creature, double thresholdFraction)
        {
            // clamp threshold between 0 and 1
            var frac = Math.Clamp(thresholdFraction, 0.0, 1.0);
            _thresholds[creature] = frac;
            creature.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(object? sender, HealthChangedEventArgs e)
        {
            if (sender is not ICreature creature) return;

            // skip if not in our map or already dead
            if (!_thresholds.TryGetValue(creature, out var thresholdFraction)) return;
            if (e.NewHp <= 0) return;

            int thresholdHitPoints = (int)(creature.MaxHitPoints * thresholdFraction);

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
