using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Admin.Model
{
    [DataContract]
    public class StatContract
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IReadOnlyDictionary<string, long> Metrics { get; set; }

        [DataMember]
        public StatMode StatMode { get; set; }
    }
}
