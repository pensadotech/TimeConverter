using System;
using System.Runtime.Serialization;

namespace Ptech.Core.SerializationLibrary.Test.Dto
{
    [Serializable]
    [DataContract(Name = "UserConfigVariable", Namespace = "ptech/2019/UserConfiguration")]
    public class UserConfigVariable
    {
        // Properties
        [DataMember(Name = "GroupKey", IsRequired = false)]
        public String GroupKey { get; set; }

        [DataMember(Name = "SubgroupKey", IsRequired = false)]
        public String SubgroupKey { get; set; }

        [DataMember(Name = "KeyName", IsRequired = false)]
        public String KeyName { get; set; }

        [DataMember(Name = "KeyValue1", IsRequired = false)]
        public String KeyValue1 { get; set; }

        [DataMember(Name = "KeyValue2", IsRequired = false)]
        public String KeyValue2 { get; set; }


    }
}
