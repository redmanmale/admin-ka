using System.Runtime.Serialization;

namespace Admin.Model
{
    [DataContract]
    public class ModuleInfo
    {
        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public string ModuleVersion { get; set; }

        [DataMember]
        public string[] Workers { get; set; }

        public override string ToString() => $"{ModuleName} v.{ModuleVersion} ";
    }
}
