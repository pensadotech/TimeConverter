using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TimeConverter.Domain.Dto
{
    /// <summary>
    /// This class represent a domain object to handle user preferences for the
    /// applicaiton and will be refernced by any frontend or Data-Access layer
    /// and to keep teh layeres independet, this will refrence these DTOs
    /// or even translate to their local DTO to facilitate the dependency injection
    /// architecture. 
    /// </summary>
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
