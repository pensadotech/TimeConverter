using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TimeConverter.DataAccess.Entities
{
    [Serializable, XmlRoot("SystemConfig")]
    public class SystemConfig
    {
        // Properties ................................................
        public string ConfigFilename { get; set; }
        [XmlArray("ConfigItems")]
        public List<SystemConfigItem> ConfigItems { get; set; }

        // Constructors ..............................................
        public SystemConfig()
        {
        }

        public SystemConfig(string configFilename) : this()
        {
            this.ConfigFilename = configFilename;
            ConfigItems = new List<SystemConfigItem>();
        }

        // Methods ....................................................

        public string GetConfigItemValue(string configKey)
        {
            string itemValue = String.Empty;

            SystemConfigItem cfgItem = ConfigItems.FirstOrDefault(s => s.ConfigKey == configKey);
            if (cfgItem != null)
            {
                itemValue = cfgItem.ConfigValue;

            }

            return itemValue;
        }

        public void SetConfigItem(string configKey, string configValue)
        {
            SystemConfigItem cfgItem = new SystemConfigItem()
            {
                ConfigKey = configKey,
                ConfigValue = configValue
            };

            // Save or Update setting item
            SetConfigItem(cfgItem);
        }

        private void SetConfigItem(SystemConfigItem configItem)
        {
            // determine if the config item already exists
            SystemConfigItem cfgItem = ConfigItems.FirstOrDefault(s => s.ConfigKey == configItem.ConfigKey);

            // Create or update 
            if (cfgItem == null)
            {
                cfgItem = new SystemConfigItem()
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
