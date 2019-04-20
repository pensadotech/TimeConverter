using System;
using System.Xml.Serialization;

namespace TimeConverter.Domain.Dto
{
    /// <summary>
    /// DTO tha represent individual configuraiton items
    /// part of the overall configuraiton DTO
    /// </summary>
    public class UserConfigItem
    {
        // Properties ..............................
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }

        // Constructors ...........................
        public UserConfigItem()
        {
            // nothing 
        }

        public UserConfigItem(string configKey, string configValue)
        {
            this.ConfigKey = configKey;
            this.ConfigValue = configValue;
        }
    }
}
