using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ptech.Core.SerializationLibrary.Test.Dto
{
    /// <summary>
    /// Local DTO for testing purposes only
    /// </summary>
    //[XmlRoot("UserConfiguration", Namespace = "http://www.PTech.com")]
    [Serializable]
    [DataContract(Name = "UserConfiguration", Namespace = "ptech/2019/UserConfiguration")]
    public class UserConfiguration
    {
        [XmlElement("ConfigName")]
        [DataMember(Name = "ConfigName", IsRequired = true)]
        public String ConfigName { get; set; }

        //[XmlAttribute("SavedDateTime")]
        [XmlElement("LastSaveDateTime")]
        [DataMember(Name = "LastSaveDateTime", IsRequired = true)]
        public DateTime SavedDateTime { get; set; }

        [XmlArrayAttribute("UserVariables")]
        [DataMember(Name = "userConfigVariables", IsRequired = false)]
        public List<UserConfigVariable> UserConfigVariables { get; set; }


        public UserConfiguration()
        {
            UserConfigVariables = new List<UserConfigVariable>();
        }

        public void AddConfigVar(string grpKey, string SubgrpKey, string keyName, string keyVal1, string keyVal2)
        {   
            // Create a new configuration item
            UserConfigVariable cfgVar = new UserConfigVariable()
            {
                GroupKey = grpKey,
                SubgroupKey = SubgrpKey,
                KeyName = keyName,
                KeyValue1 = keyVal1,
                KeyValue2 = keyVal2
            };
            
            // Add to teh list of configurat
            UserConfigVariables.Add(cfgVar);

        }


    }
}
