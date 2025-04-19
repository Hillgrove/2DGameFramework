using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Services;

namespace _2DGameFramework.Core.Creatures
{
    /// <summary>
    /// Represents a creature in the world that can move, attack, receive damage, heal, and loot items.
    /// </summary>
    public class Creature : WorldObject, ICreature
    {
        public int HitPoints { get; internal set; }
        public int MaxHitPoints { get; }
        public Position Position { get; internal set; }

        private readonly ICombatService _combatService;
        private readonly IMovementService _movementService;
        private readonly IInventoryService _inventoryService;
        private readonly IStatsService _statsService;


        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">An optional description of the creature.</param>
        /// <param name="hitpoints">The starting and maximum hit points of the creature.</param>
        /// <param name="startPosition">The initial position of the creature in the world.</param>
        /// <param name="logger">The logger used to record game events.</param>
        public Creature(
            string name,
            string description,
            int hitpoints,
            Position startPosition,
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventoryService inventoryService)
                : base(name, description)
        {
            HitPoints = hitpoints;
            MaxHitPoints = hitpoints;
            Position = startPosition;
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventoryService = inventoryService;
        }

        /// <inheritdoc />
        public void Attack(ICreature target) => _combatService.Attack(this, target);

        /// <inheritdoc />
        public void AdjustHitPoints(int delta)
        {
            HitPoints = Math.Max(0, Math.Min(HitPoints + delta, MaxHitPoints));
        }

        /// <inheritdoc />
        public void MoveBy(int deltaX, int deltaY, World world) 
            => Position = _movementService.Move(Position, deltaX, deltaY, world);

        /// <inheritdoc />
        public IEnumerable<IUsable> GetUsables() => _inventoryService.GetUsables();

        /// <inheritdoc />
        public void Loot(ILootSource source, World world) => _inventoryService.Loot(this, source, world);

        /// <inheritdoc />
        public void UseItem(WorldObject item) => _inventoryService.UseItem(this, item);

        ///<inheritdoc/>
        public int GetTotalBaseDamage() => _statsService.GetTotalBaseDamage();

        ///<inheritdoc/>
        public int GetTotalDamageReduction() => _statsService.GetTotalDamageReduction();

        /// <inheritdoc />
        public void EquipWeapon(IDamageSource weapon) => _inventoryService.EquipAttackItem(weapon);

        /// <inheritdoc />
        public void EquipArmor(IDefenseSource armor) => _inventoryService.EquipDefenseItem(armor);

        /// <summary>
        /// Returns a string representation of the creature’s current state, including position and hit points.
        /// </summary>
        /// <returns>A formatted string describing this creature.</returns>
        public override string ToString() =>
            $"{Name} at {Position} ({HitPoints}/{MaxHitPoints} HP)";


    }
}
