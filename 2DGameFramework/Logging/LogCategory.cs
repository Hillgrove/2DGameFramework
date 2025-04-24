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
        Movement        = 3000,
        Combat          = 4000,
        Inventory       = 5000,
        Configuration   = 6000,
        Error           = 9000
    }
}
