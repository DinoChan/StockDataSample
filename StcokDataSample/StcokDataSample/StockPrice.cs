using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace StcokDataSample
{
    [Serializable]
    [ProtoContract]
    [DataContract]
    public class StockPrice
    {
        [ProtoMember(1)]
        [DataMember]
        public double ClosePrice { get; set; }

        [ProtoMember(2)]
        [DataMember]
        public DateTime Date { get; set; }

        [ProtoMember(3)]
        [DataMember]
        public double HighPrice { get; set; }

        [ProtoMember(4)]
        [DataMember]
        public double LowPrice { get; set; }

        [ProtoMember(5)]
        [DataMember]
        public double OpenPrice { get; set; }

        [ProtoMember(6)]
        [DataMember]
        public double PrvClosePrice { get; set; }

        [ProtoMember(7)]
        [DataMember]
        public string Symbol { get; set; }

        [ProtoMember(8)]
        [DataMember]
        public double Turnover { get; set; }

        [ProtoMember(9)]
        [DataMember]
        public double Volume { get; set; }
    }
}