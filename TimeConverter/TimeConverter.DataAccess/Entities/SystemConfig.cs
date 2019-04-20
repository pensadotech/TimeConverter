using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TimeConverter.DataAccess.Entities
{
    /// <summary>
    /// Data-Access object that will be used to save or load configuration data to/from the local driver
    /// This data objects belongs only to teh Data-Access layer but will has funcitonality
    /// to tarnslate to/from a Domain object to keep logical layers separated. 
    /// </summary>
    [Serializable, XmlRoot("SystemConfig")]
    [DataContract(Name = "UserConfiguration", Namespace = "ptech/2019/UserConfiguration")]
    public class SystemConfig
    {
        // Properties ................................................
        [XmlElement("ConfigName")]
        [DataMember(Name = "ConfigName", IsRequired = true)]
        public string ConfigFilename { get; set; }

        [XmlElement("LastSaveDateTime")]
        [DataMember(Name = "LastSaveDateTime", IsRequired = true)]
        public DateTime SavedDateTime { get; set; }

        [XmlArrayAttribute("UserConfigItems")]
        [DataMember(Name = "UserConfigItems", IsRequired = false)]
        public List<SystemConfigItem> ConfigItems { get; set; }

        // Constructors ..............................................
        public SystemConfig()
        {
            ConfigItems = new List<SystemConfigItem>();
        }

        public SystemConfig(string configFilename) : this()
        {
            this.ConfigFilename = configFilename;
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

        /// <summary>
        /// Convert configuration object to domain config object
        /// </summary>
        /// <returns></returns>
        public Domain.Dto.UserConfiguration ToDomainObject()
        {
            var userConfig = new Domain.Dto.UserConfiguration
            {
                ConfigFilename = this.ConfigFilename,
                SavedDateTime = this.SavedDateTime
            };

            // Convert each local system config into domain config items
            foreach (var item in this.ConfigItems)
            {
                userConfig.SetConfigItem(item.ConfigKey, item.ConfigValue);
            }

            return userConfig;
        }

        /// <summary>
        /// Convert Domain user configuration object into local system config object
        /// </summary>
        /// <param name="userConfig"></param>
        public void ToDataAccessObject(Domain.Dto.UserConfiguration userConfig)
        {
            // Set configuration file name
            this.ConfigFilename = userConfig.ConfigFilename;
            this.SavedDateTime = DateTime.Now;

            // Convert each domain user config item into local config item
            foreach (var item in userConfig.ConfigItems)
            {
                SetConfigItem(item.ConfigKey, item.ConfigValue);
            }
        }

    }
}
