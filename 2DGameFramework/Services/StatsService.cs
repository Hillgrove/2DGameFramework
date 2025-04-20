using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Services
{
    public class StatsService : IStatsService
    {
        private readonly IInventoryService _inventory;

        public StatsService(IInventoryService inventory)
        {
            _inventory = inventory;
        }

        public int GetTotalBaseDamage()
          => _inventory.GetAttackItems().Sum(i => i.BaseDamage);

        public int GetTotalDamageReduction()
          => _inventory.GetDefenseItems().Sum(d => d.DamageReduction);
    }
}
