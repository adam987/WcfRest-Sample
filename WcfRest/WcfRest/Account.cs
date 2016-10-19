using System;
using System.Runtime.Serialization;

namespace WcfRest
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public Guid Guid { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        public Account()
        {
            CreationDate = DateTime.Now;
        }
    }
}