using System;
using System.Xml.Serialization;

namespace TimeConverter.Domain.Dto
{
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
