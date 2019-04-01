using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TimeConverter.Domain.Dto
{
    [Serializable, XmlRoot("ApplicationConfig")]
    public class ApplicationConfig
    {
        // Properties ................................................
        public string ConfigFilename { get; set; }
        [XmlArray("ConfigItems")]
        public List<ConfigItem> ConfigItems { get; set; }

        // Constructors ..............................................
        public ApplicationConfig()
        {

        }

        public ApplicationConfig(string configFilename) : this()
        {
            this.ConfigFilename = configFilename;
            ConfigItems = new List<ConfigItem>();
        }

        // Methods ....................................................

        public string GetConfigItemValue(string configKey)
        {
            string itemValue = String.Empty;

            ConfigItem cfgItem = ConfigItems.FirstOrDefault(s => s.ConfigKey == configKey);
            if (cfgItem != null)
            {
                itemValue = cfgItem.ConfigValue;

            }

            return itemValue;
        }

        public void SetConfigItem(string configKey, string configValue)
        {
            ConfigItem cfgItem = new ConfigItem()
            {
                ConfigKey = configKey,
                ConfigValue = configValue
            };

            // Save or Update setting item
            SetConfigItem(cfgItem);
        }

        private void SetConfigItem(ConfigItem configItem)
        {
            // determine if the config item already exists
            ConfigItem cfgItem = ConfigItems.FirstOrDefault(s => s.ConfigKey == configItem.ConfigKey);

            // Create or update 
            if (cfgItem == null)
            {
                cfgItem = new ConfigItem()
                {
                    ConfigKey = configItem.ConfigKey,
                    ConfigValue = configItem.ConfigValue
                };

                // Add element to the list 
                ConfigItems.Add(cfgItem);

            }
            else
            {
                // Update exiting element in list 
                cfgItem.ConfigValue = configItem.ConfigValue;
            }
        }
    }
}
