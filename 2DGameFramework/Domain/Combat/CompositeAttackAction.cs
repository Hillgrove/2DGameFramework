using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Combat
{
    /// <summary>
    /// Composite of multiple attack actions: executes each in turn.
    /// </summary>
    public class CompositeAttackAction : IAttackAction
    {
        private readonly List<IAttackAction> _children = new();

        public void Add(IAttackAction action) => _children.Add(action);
        public void Remove(IAttackAction action) => _children.Remove(action);

        public void Execute(ICreature attacker, ICreature target)
        {
            foreach (var action in _children)
                action.Execute(attacker, target);
        }
    }
}
