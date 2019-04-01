using System;
using System.Xml.Serialization;

namespace TimeConverter.Service.Configuration
{
    [Serializable]
    public class ConfigItem
    {
        // Properties ..............................
        [XmlElement("ConfigKey")]
        public string ConfigKey { get; set; }
        [XmlElement("ConfigValue")]
        public string ConfigValue { get; set; }

        // Constructors ...........................
        public ConfigItem()
        {
            // nothing 
        }

        public ConfigItem(string configKey, string configValue)
        {
            this.ConfigKey = configKey;
            this.ConfigValue = configValue;
        }
    }
}
