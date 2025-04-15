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

            GameConfig config = new()
            {
                WorldWidth = int.Parse(configElement.Element("WorldWidth")?.Value ?? "100"),
                WorldHeight = int.Parse(configElement.Element("WorldHeight")?.Value ?? "100"),
                GameLevel = Enum.TryParse(configElement.Element("GameLevel")?.Value?.Trim(), ignoreCase: true, out GameLevel level)
                            ? level
                            : GameLevel.Normal
            };

            return config;
        }
    }
}
