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
        private static DateTime _beginDate = new DateTime(1970, 1, 1);

        [ProtoMember(1)]
        [DataMember]
        public  float ClosePrice { get; set; }



        public  DateTime Date {
            get => _beginDate.AddDays(DaysFrom1970);
            set => DaysFrom1970 = (short) Math.Floor((value - _beginDate).TotalDays);
        }


        [ProtoMember(2)]
        [DataMember]
        public short DaysFrom1970 { get; set; }

        [ProtoMember(3)]
        [DataMember]
        public  float HighPrice { get; set; }

        [ProtoMember(4)]
        [DataMember]
        public  float LowPrice { get; set; }

        [ProtoMember(5)]
        [DataMember]
        public  float OpenPrice { get; set; }

        [ProtoMember(6)]
        [DataMember]
        public  float PrvClosePrice { get; set; }

        [ProtoMember(8)]
        [DataMember]
        public  double Turnover { get; set; }

        [ProtoMember(9)]
        [DataMember]
        public  int Volume { get; set; }
        
    }
}
