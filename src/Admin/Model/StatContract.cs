using System.Runtime.Serialization;

namespace Admin.Model
{
    [DataContract]
    public class StatContract
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long SpeedIns { get; set; }

        [DataMember]
        public long SpeedAvg { get; set; }

        [DataMember]
        public long CountAll { get; set; }

        [DataMember]
        public long CountInQ { get; set; }

        [DataMember]
        public bool IsSpeed { get; set; }
    }
}
