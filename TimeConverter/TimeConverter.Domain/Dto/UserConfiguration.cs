using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TimeConverter.Domain.Dto
{
    public class UserConfiguration
    {
        // Properties ................................................
        public string ConfigFilename { get; set; }
        public List<UserConfigItem> ConfigItems { get; set; }
        public DateTime SavedDateTime { get; set; }

        // Constructors ..............................................
        public UserConfiguration()
        {
            ConfigItems = new List<UserConfigItem>();
        }

        public UserConfiguration(string configFilename) : this()
        {
            this.ConfigFilename = configFilename;
        }

        // Methods ....................................................

        public string GetConfigItemValue(string configKey)
        {
            string itemValue = String.Empty;

            UserConfigItem cfgItem = ConfigItems.FirstOrDefault(s => s.ConfigKey == configKey);
            if (cfgItem != null)
            {
                itemValue = cfgItem.ConfigValue;

            }

            return itemValue;
        }

        public void SetConfigItem(string configKey, string configValue)
        {
            UserConfigItem cfgItem = new UserConfigItem()
            {
                ConfigKey = configKey,
                ConfigValue = configValue
            };

            // Save or Update setting item
            SetConfigItem(cfgItem);
        }

        private void SetConfigItem(UserConfigItem configItem)
        {
            // determine if the config item already exists
            UserConfigItem cfgItem = ConfigItems.FirstOrDefault(s => s.ConfigKey == configItem.ConfigKey);

            // Create or update 
            if (cfgItem == null)
            {
                cfgItem = new UserConfigItem()
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
