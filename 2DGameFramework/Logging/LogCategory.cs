namespace _2DGameFramework.Logging
{
    /// <summary>
    /// Defines categories for log messages, allowing trace events to be grouped
    /// and filtered by area of concern within the framework.
    /// </summary>
    public enum LogCategory
    {
        Game            = 1000,
        World           = 2000,
        Combat          = 3000,
        Inventory       = 4000,
        Configuration   = 5000,
        Error           = 9000
    }
}
