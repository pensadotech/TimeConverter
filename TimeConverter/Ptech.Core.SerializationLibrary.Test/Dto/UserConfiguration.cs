using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ptech.Core.SerializationLibrary.Test.Dto
{
    //[XmlRoot("UserConfiguration", Namespace = "http://www.PTech.com")]
    [Serializable]
    [DataContract(Name = "UserConfiguration", Namespace = "ptech/2019/UserConfiguration")]
    public class UserConfiguration
    {
        [XmlElement("ConfigName")]
        [DataMember(Name = "ConfigName", IsRequired = true)]
        public String ConfigName;

        //[XmlAttribute("SavedDateTime")]
        [XmlElement("LastSaveateTime")]

        [DataMember(Name = "SavedDateTime", IsRequired = true)]
        public String SavedDateTime;

        [XmlArrayAttribute("UserVariables")]
        [DataMember(Name = "userConfigVariables", IsRequired = false)]
        public List<UserConfigVariable> userConfigVariables;


        public UserConfiguration()
        {
            userConfigVariables = new List<UserConfigVariable>();
        }

        public void AddConfigVar(string grpKey, string SubgrpKey, string keyName, string keyVal1, string keyVal2)
        {
            UserConfigVariable cfgVar = new UserConfigVariable();
            cfgVar.GroupKey = grpKey;
            cfgVar.SubgroupKey = SubgrpKey;
            cfgVar.KeyName = keyName;
            cfgVar.KeyValue1 = keyVal1;
            cfgVar.KeyValue2 = keyVal2;

            userConfigVariables.Add(cfgVar);

        }


    }
}
