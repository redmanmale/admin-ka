using System.Runtime.Serialization;

namespace Admin.Model
{
    [DataContract]
    public class Info
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public bool IsDanger { get; set; } 
    }
}
