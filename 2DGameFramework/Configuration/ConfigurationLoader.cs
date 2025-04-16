using _2DGameFramework.Logging;
using _2DGameFramework.Models.Base;
using System.Diagnostics;
using System.Xml.Linq;


namespace _2DGameFramework.Configuration
{
    public static class ConfigurationLoader
    {
        public static GameConfig Load(string filePath)
        {
            GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, $"Attempting to load configuration file: {filePath}", offset: 1);

            if (!File.Exists(filePath))
            {
                GameLogger.Log(TraceEventType.Error, LogCategory.Configuration, $"Config file not found: {filePath}", offset: 2);
                throw new FileNotFoundException($"Configuration file not found: {filePath}");
            }

            XDocument doc = XDocument.Load(filePath);
            GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, $"Successfully loaded configuration file: {filePath}", offset: 3);
            
            var configElement = doc.Element("Configuration");

            if (configElement == null)
            {
                GameLogger.Log(TraceEventType.Error, LogCategory.Configuration, "Invalid XML format: Root element <Configuration> missing", offset: 4);
                throw new Exception("Invalid XML format. Root must be <Configuration>");
            }

            GameConfig config = new();
            GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, "Beginning to parse configuration values", offset: 5);

            var widthNode = configElement.Element("WorldWidth");
            if (widthNode != null)
            {
                config.WorldWidth = int.Parse(widthNode.Value);
                GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, $"WorldWidth set to {config.WorldWidth}", offset: 6);
            }

            var heightNode = configElement.Element("WorldHeight");
            if (heightNode != null)
            {
                config.WorldHeight = int.Parse(heightNode.Value);
                GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, $"WorldHeight set to {config.WorldHeight}", offset: 7);
            }

            var levelNode = configElement.Element("GameLevel");
            if (levelNode != null && Enum.TryParse(levelNode.Value.Trim(), true, out GameLevel level))
            {
                config.GameLevel = level;
                GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, $"GameLevel set to {config.GameLevel}", offset: 8);
            }

            GameLogger.Log(TraceEventType.Information, LogCategory.Configuration, "Configuration parsing complete", offset: 9);
            return config;
        }
    }
}
