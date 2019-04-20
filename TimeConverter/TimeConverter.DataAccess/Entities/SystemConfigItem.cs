using System;
using System.Xml.Serialization;
using Domain = TimeConverter.Domain;

namespace TimeConverter.DataAccess.Entities
{
    /// <summary>
    /// Data-Access object that will be used to save or load configuration data to/from the local driver
    /// This data objects belongs only to teh Data-Access layer but will has funcitonality
    /// to tarnslate to/from a Domain object to keep logical layers separated. 
    /// </summary>
    [Serializable]
    public class SystemConfigItem
    {
        // Properties ..............................
        [XmlElement("ConfigKey")]
        public string ConfigKey { get; set; }
        [XmlElement("ConfigValue")]
        public string ConfigValue { get; set; }

        // Constructors ...........................
        public SystemConfigItem()
        {
            // nothing 
        }

        public SystemConfigItem(string configKey, string configValue)
        {
            this.ConfigKey = configKey;
            this.ConfigValue = configValue;
        }

        // Methods .................................
        public Domain.Dto.UserConfigItem ToDoaminSysteVariable()
        {
            var userConfigItem = new Domain.Dto.UserConfigItem
            {
                ConfigKey = this.ConfigKey,
                ConfigValue = this.ConfigValue
            };

            return userConfigItem;
        }

        public void ToDataAccessVariable(Domain.Dto.UserConfigItem userConfigItem)
        {
            if (userConfigItem != null)
            {
                this.ConfigKey = userConfigItem.ConfigKey;
                this.ConfigValue = userConfigItem.ConfigValue;
            }
        }

    }
}
