namespace _2DGameFramework.Core.Creatures
{
    /// <summary>
    /// Event arguments for when a creature dies.
    /// </summary>
    public class DeathEventArgs : EventArgs
    {
        public ICreature DeadCreature { get; }
        public DeathEventArgs(ICreature dead) => DeadCreature = dead;
    }
}
