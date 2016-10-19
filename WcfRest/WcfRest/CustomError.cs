using System.Runtime.Serialization;

namespace WcfRest
{
    [DataContract]
    public class CustomError
    {
        [DataMember]
        public Account Account { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}