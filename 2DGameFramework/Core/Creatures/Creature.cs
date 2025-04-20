using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Services;


namespace _2DGameFramework.Core.Creatures
{
    
    /// <summary>
    /// Represents a creature in the world that can move, attack, receive damage, heal, and loot items.
    /// </summary>
    public abstract class Creature : WorldObject, ICreature
    {
        public int HitPoints { get; internal set; }
        public int MaxHitPoints { get; }
        public Position Position { get; internal set; }

        protected readonly ICombatService _combatService;
        protected readonly IMovementService _movementService;
        protected readonly IInventoryService _inventoryService;
        protected readonly IStatsService _statsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">An optional description of the creature.</param>
        /// <param name="hitpoints">The starting and maximum hit points of the creature.</param>
        /// <param name="startPosition">The initial position of the creature in the world.</param>
        /// <param name="statsService">The stats service to calculate damage and reduction.</param>
        /// <param name="combatService">The combat service to handle attack logic.</param>
        /// <param name="movementService">The movement service to handle positioning.</param>
        /// <param name="inventoryService">The inventory service to handle items.</param>
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

        #region Public Methods
        /// <inheritdoc />
        public void Attack(ICreature target) => AttackTemplate(target);

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
        public void UseItem(IUsable item) => _inventoryService.UseItem(this, item);

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
        #endregion

        #region Template Method Hooks
        /// <summary>
        /// Template method that orchestrates the attack sequence:
        /// calls PreAttack, DoAttack, then PostAttack in order.
        /// </summary>
        /// <param name="target">The creature to be attacked.</param>
        protected void AttackTemplate(ICreature target)
        {
            PreAttack(target);
            DoAttack(target);
            PostAttack(target);
        }

        /// <summary>
        /// Hook invoked before the actual attack logic. 
        /// Subclasses can override to add pre‐attack behavior.
        /// </summary>
        /// <param name="target">The creature to be attacked.</param>
        protected virtual void PreAttack(ICreature target)
        {
            // e.g. throw if out of range, consume stamina, apply “first‑strike” buff
        }

        /// <summary>
        /// Core attack step. Subclasses **must** implement this
        /// to perform the actual damage application.
        /// </summary>
        /// <param name="target">The creature to be attacked.</param>
        protected abstract void DoAttack(ICreature target);

        /// <summary>
        /// Hook invoked after the actual attack logic. 
        /// Subclasses can override to add post‐attack behavior.
        /// </summary>
        /// <param name="target">The creature that was attacked.</param>
        protected virtual void PostAttack(ICreature target)
        {
            // e.g. trigger OnHit observers, apply bleed effect, log damage summary
        }
        #endregion
    }
}
