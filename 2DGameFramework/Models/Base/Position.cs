namespace _2DGameFramework.Models.Base
{
    /// <summary>
    /// Represents a two‑dimensional coordinate in the game world.
    /// </summary>
    public readonly record struct Position(int X, int Y)
    {
        /// <summary>
        /// Returns a string formatted as "(X, Y)", where X and Y are the coordinate values.
        /// </summary>
        /// <returns>A string representation of the position.</returns>
        public override string ToString() => $"({X}, {Y})";
    }
}
