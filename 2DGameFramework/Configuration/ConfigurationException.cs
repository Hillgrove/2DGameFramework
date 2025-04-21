namespace _2DGameFramework.Configuration
{
    /// <summary>
    /// Thrown when something is wrong in the configuration file.
    /// </summary>
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the configuration error.</param>
        public ConfigurationException(string message) : base(message) { }
    }
}
