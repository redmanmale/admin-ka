using System.Runtime.Serialization;

namespace Admin.Model
{
    [DataContract]
    public enum StatMode
    {
        [EnumMember]
        Count = 0,

        [EnumMember]
        Speed = 1
    }
}
