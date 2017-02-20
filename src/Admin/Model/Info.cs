using System.Runtime.Serialization;

namespace Admin.Model
{
    [DataContract]
    public class Info
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value1 { get; set; }

        [DataMember]
        public string Value2 { get; set; }

        [DataMember]
        public int Mode { get; set; } 
    }
}
