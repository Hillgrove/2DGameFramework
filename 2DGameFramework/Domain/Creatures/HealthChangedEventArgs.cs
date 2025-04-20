namespace _2DGameFramework.Domain.Creatures
{
    /// <summary>
    /// Data for when a creature’s health changes.
    /// </summary>
    public class HealthChangedEventArgs : EventArgs
    {
        public int OldHp { get; }
        public int NewHp { get; }

        public HealthChangedEventArgs(int oldHp, int newHp)
        {
            OldHp = oldHp;
            NewHp = newHp;
        }
    }
}
