using _2DGameFramework.Core;
using _2DGameFramework.Domain.Combat;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Services;


namespace _2DGameFramework.Domain.Creatures
{

    /// <summary>
    /// Represents a creature in the world that can move, attack, receive damage, heal, and loot items.
    /// </summary>
    public abstract class Creature : WorldObject, ICreature
    {
        public int HitPoints { get; internal set; }
        public int MaxHitPoints { get; }
        public Position Position { get; internal set; }
        public IInventoryService Inventory => _inventory;

        protected readonly ICombatService _combatService;
        protected readonly IMovementService _movementService;
        protected readonly IInventoryService _inventory;
        protected readonly IStatsService _statsService;

        protected readonly Dictionary<string, IAttackAction> _namedActions = new();

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
            _inventory = inventoryService;
        }

        #region Public Methods
        /// <summary>
        /// Registers an attack action under a unique key.
        /// CompositeAttackAction can itself wrap multiple actions.
        /// </summary>
        public void RegisterAttackAction(string key, IAttackAction action)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Action key must be non-empty", nameof(key));
            _namedActions[key] = action;
        }

        /// <inheritdoc />
        public void Attack(string actionKey, ICreature target)
        {
            if (!_namedActions.TryGetValue(actionKey, out var action))
                throw new InvalidOperationException($"No attack registered with key '{actionKey}'");

            AttackTemplate(action, target);
        }

        /// <inheritdoc />
        public void AdjustHitPoints(int delta)
        {
            int oldHitPoints = HitPoints;
            HitPoints = Math.Max(0, Math.Min(HitPoints + delta, MaxHitPoints));

            // send notification
            HealthChanged?.Invoke(this, new HealthChangedEventArgs(oldHitPoints, HitPoints));

            // fire OnDeath when crossing from alive (>0) to dead (0)
            if (oldHitPoints > 0 && HitPoints <= 0)
                OnDeath?.Invoke(this, new DeathEventArgs(this));
        }

        /// <inheritdoc />
        public void MoveBy(int deltaX, int deltaY, GameWorld world)
            => Position = _movementService.Move(this, Position, deltaX, deltaY, world);

        /// <inheritdoc />
        public IEnumerable<IConsumable> GetUsables() => _inventory.GetUsables();

        /// <inheritdoc />
        public void Loot(ILootSource source, GameWorld world) => _inventory.Loot(this, source, world);

        /// <inheritdoc />
        public void UseItem(IConsumable item) => _inventory.UseItem(this, item);

        ///<inheritdoc/>
        public int GetTotalBaseDamage() => _statsService.GetTotalBaseDamage();

        ///<inheritdoc/>
        public int GetTotalDamageReduction() => _statsService.GetTotalDamageReduction();

        /// <inheritdoc />
        public void EquipWeapon(IDamageSource weapon) => _inventory.EquipAttackItem(weapon);

        /// <inheritdoc />
        public void EquipArmor(IDefenseSource armor) => _inventory.EquipDefenseItem(armor);

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
        protected void AttackTemplate(IAttackAction action, ICreature target)
        {
            PreAttack(action, target);
            DoAttack(action, target);
            PostAttack(action, target);
        }

        /// <summary>
        /// Hook invoked before the actual attack logic. 
        /// Subclasses can override to add pre‐attack behavior.
        /// </summary>
        /// <param name="target">The creature to be attacked.</param>
        protected virtual void PreAttack(IAttackAction action, ICreature target)
        {
            // e.g. throw if out of range, consume stamina, apply “first‑strike” buff
        }

        /// <summary>
        /// Core attack step. Subclasses **must** implement this
        /// to perform the actual damage application.
        /// </summary>
        /// <param name="target">The creature to be attacked.</param>
        protected abstract void DoAttack(IAttackAction action, ICreature target);

        /// <summary>
        /// Hook invoked after the actual attack logic. 
        /// Subclasses can override to add post‐attack behavior.
        /// </summary>
        /// <param name="target">The creature that was attacked.</param>
        protected virtual void PostAttack(IAttackAction action, ICreature target)
        {
            // e.g. trigger OnHit observers, apply bleed effect, log damage summary
        }
        #endregion

        #region Events
        /// <summary>
        /// Fired after HP is adjusted.  
        /// Subscribers can inspect OldHp and NewHp to react (e.g. auto‑heal).
        /// </summary>
        public event EventHandler<HealthChangedEventArgs>? HealthChanged;

        /// <summary>
        /// Fired once, the moment this creature’s HP falls to zero.
        /// </summary>
        public event EventHandler<DeathEventArgs>? OnDeath;
        #endregion
    }
}
