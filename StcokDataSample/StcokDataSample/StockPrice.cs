using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
        public virtual double AdjustClosePrice { get; set; }

        [ProtoMember(2)]
        [DataMember]
        public virtual double ClosePrice { get; set; }

        [ProtoMember(3)]
        [DataMember]
        public virtual DateTime Date { get; set; }


        [ProtoMember(4)]
        [DataMember]
        public virtual string Guid { get; set; }
    
        [ProtoMember(5)]
        [DataMember]
        public virtual double HighPrice { get; set; }


        [ProtoMember(6)]
        [DataMember]
        public virtual int Id { get; set; }


        [ProtoMember(7)]
        [DataMember]
        public virtual double LowPrice { get; set; }


        [ProtoMember(8)]
        [DataMember]
        public virtual string Market { get; set; }

        [ProtoMember(9)]
        [DataMember]
        public virtual double OpenPrice { get; set; }

        [ProtoMember(10)]
        [DataMember]
        public virtual double PrvClosePrice { get; set; }

        [ProtoMember(11)]
        [DataMember]
        public virtual string Symbol { get; set; }

        [ProtoMember(12)]
        [DataMember]
        public virtual double Turnover { get; set; }

        [ProtoMember(13)]
        [DataMember]
        public virtual double Volume { get; set; }
    }
}
