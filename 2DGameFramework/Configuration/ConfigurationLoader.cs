using _2DGameFramework.Models;
using System.Xml.Linq;


namespace _2DGameFramework.Configuration
{
    public static class ConfigurationLoader
    {
        public static GameConfig Load(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Configuration file not found: {filePath}");

            XDocument doc = XDocument.Load(filePath);
            var configElement = doc.Element("Configuration");

            if (configElement == null)
                throw new Exception("Invalid XML format. Root must be <Configuration>");

            GameConfig config = new();

            var widthNode = configElement.Element("WorldWidth");
            if (widthNode != null)
                config.WorldWidth = int.Parse(widthNode.Value);

            var heightNode = configElement.Element("WorldHeight");
            if (heightNode != null)
                config.WorldHeight = int.Parse(heightNode.Value);

            var levelNode = configElement.Element("GameLevel");
            if (levelNode != null && Enum.TryParse(levelNode.Value.Trim(), true, out GameLevel level))
                config.GameLevel = level;

            return config;
        }
    }
}
