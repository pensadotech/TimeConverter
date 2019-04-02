using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Domain = TimeConverter.Domain;

namespace TimeConverter.DataAccess.Entities
{
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
