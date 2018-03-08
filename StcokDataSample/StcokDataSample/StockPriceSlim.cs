using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using System.IO;

namespace StcokDataSample
{
    [Serializable]
    [ProtoContract]
    [DataContract]
    public class StockPriceSlim
    {
        [ProtoMember(1)]
        [DataMember]
        public virtual float ClosePrice { get; set; }

        [ProtoMember(2)]
        [DataMember]
        public virtual DateTime Date { get; set; }

        [ProtoMember(3)]
        [DataMember]
        public virtual float HighPrice { get; set; }

        [ProtoMember(4)]
        [DataMember]
        public virtual float LowPrice { get; set; }

        [ProtoMember(5)]
        [DataMember]
        public virtual float OpenPrice { get; set; }

        [ProtoMember(6)]
        [DataMember]
        public virtual float PrvClosePrice { get; set; }

        [ProtoMember(8)]
        [DataMember]
        public virtual double Turnover { get; set; }

        [ProtoMember(9)]
        [DataMember]
        public virtual int Volume { get; set; }

       
    }
}
