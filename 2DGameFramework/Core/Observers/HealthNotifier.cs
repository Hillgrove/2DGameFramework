using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Services;

namespace _2DGameFramework.Core.Observers
{
    /// <summary>
    /// Listens for low‑HP on creatures and auto‑uses a healing item once below threshold.
    /// </summary>
    public class HealthNotifier
    {
        private readonly IInventoryService      _inventory;
        private readonly double                 _thresholdFraction;
        private readonly HashSet<ICreature>     _alreadyNotified = new();

        public HealthNotifier(
            IInventoryService inventory,
            double thresholdFraction = 0.25)
        {
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

            // ignore of already triggered
            if (_alreadyNotified.Contains(creature)) return;

            // check current HP
            if (creature.HitPoints <= creature.MaxHitPoints * _thresholdFraction)
            {
                var healingItem = creature
                    .GetUsables()
                    .FirstOrDefault(u => (u.Type & ConsumableType.Healing) != 0);

                if (healingItem != null)
                {
                    _inventory.UseItem(creature, healingItem);
                    _alreadyNotified.Add(creature);
                }
            }
        }
    }
}
