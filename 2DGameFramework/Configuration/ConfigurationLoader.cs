using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;
using System.Xml;


namespace _2DGameFramework.Configuration
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }


    public class ConfigurationLoader
    {
        private readonly ILogger _logger;

        public ConfigurationLoader(ILogger logger)
        {
            _logger = logger;
        }

        public GameConfig Load(string xmlFile)
        {
            _logger.Log(
                TraceEventType.Information, 
                LogCategory.Configuration,
                $"Loading config: {xmlFile}");
            
            if (!File.Exists(xmlFile))
                throw new ConfigurationException($"Config file not found: {xmlFile}");

            // 1) Load the XML document
            var doc = new XmlDocument();
            doc.Load(xmlFile);

            _logger.Log(
                TraceEventType.Information, 
                LogCategory.Configuration, 
                $"Successfully loaded configuration file: {xmlFile}");

            // 2) Read and validate configuration values
            var config = new GameConfig
            {
                WorldWidth = ReadInt("/Configuration/WorldWidth"),
                WorldHeight = ReadInt("/Configuration/WorldHeight"),
                GameLevel = ReadEnum<GameLevel>("/Configuration/GameLevel")
            };

            #region Local helper functions
            // Helper to read & validate an int
            int ReadInt(string xpath)
            {
                var node = doc.SelectSingleNode(xpath) ?? throw new ConfigurationException($"Missing element: {xpath}");
                
                if (!int.TryParse(node.InnerText, out var v))
                    throw new ConfigurationException($"Invalid integer in {xpath}: '{node.InnerText}'");
                
                return v;
            }

            // Helper to read & validate an enum
            T ReadEnum<T>(string xpath) where T : struct
            {
                var node = doc.SelectSingleNode(xpath) ?? throw new ConfigurationException($"Missing element: {xpath}");
                
                if (!Enum.TryParse(node.InnerText, out T e))
                    throw new ConfigurationException($"Invalid value in {xpath}: '{node.InnerText}'");
                
                return e;
            }
            #endregion

            _logger.Log(
                TraceEventType.Information, 
                LogCategory.Configuration,
                "Config loaded successfully");
            
            return config;
        }
    }
}
